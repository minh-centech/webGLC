using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using webGLCv2.Components;
using webGLCv2.Controllers;
using webGLCv2.Models;
using webGLCv2.Services;

var builder = WebApplication.CreateBuilder(args);
var legacyApiSection = builder.Configuration.GetSection(LegacyApiOptions.SectionName);
var onlineOrderWorkflowSection = builder.Configuration.GetSection(OnlineOrderWorkflowOptions.SectionName);
var turnstileSection = builder.Configuration.GetSection(TurnstileOptions.SectionName);
var cspSection = builder.Configuration.GetSection(ContentSecurityPolicyOptions.SectionName);
var emailSenderSection = builder.Configuration.GetSection(EmailSenderOptions.SectionName);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });
builder.Services.AddAuthorization();
builder.Services.Configure<LegacyApiOptions>(legacyApiSection);
builder.Services.Configure<OnlineOrderWorkflowOptions>(onlineOrderWorkflowSection);
builder.Services.Configure<TurnstileOptions>(turnstileSection);
builder.Services.Configure<ContentSecurityPolicyOptions>(cspSection);
builder.Services.Configure<EmailSenderOptions>(emailSenderSection);
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor
        | ForwardedHeaders.XForwardedHost
        | ForwardedHeaders.XForwardedProto;

    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddHttpClient("LegacyApi", (serviceProvider, client) =>
{
    var options = legacyApiSection.Get<LegacyApiOptions>() ?? new LegacyApiOptions();

    if (string.IsNullOrWhiteSpace(options.BaseUrl))
    {
        throw new InvalidOperationException("Missing configuration: LegacyApi:BaseUrl");
    }

    client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromMinutes(5);
});

builder.Services.AddScoped<LegacyCustomerPortalService>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var emailHelper = serviceProvider.GetRequiredService<EmailHelper>();
    return new LegacyCustomerPortalService(httpClientFactory.CreateClient("LegacyApi"), emailHelper);
});

builder.Services.AddScoped<MaSoThueHelper>();

builder.Services.AddScoped<OnlineOrderService>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var workflowOptions = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<OnlineOrderWorkflowOptions>>();
    return new OnlineOrderService(httpClientFactory.CreateClient("LegacyApi"), workflowOptions);
});

builder.Services.AddScoped<EmailHelper>();

builder.Services.AddHttpClient<TurnstileValidationService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.Configure<AiProxyOptions>(
    builder.Configuration.GetSection("AiProxy"));

var viVnCulture = new CultureInfo("vi-VN");
CultureInfo.DefaultThreadCurrentCulture = viVnCulture;
CultureInfo.DefaultThreadCurrentUICulture = viVnCulture;

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(viVnCulture);
    options.SupportedCultures = new[] { viVnCulture };
    options.SupportedUICultures = new[] { viVnCulture };
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.Use(async (context, next) =>
{
    var cspOptions = context.RequestServices.GetRequiredService<IOptions<ContentSecurityPolicyOptions>>().Value;
    var enableContentSecurityPolicy = cspOptions.EnableContentSecurityPolicy && !app.Environment.IsProduction();

    if (enableContentSecurityPolicy)
    {
        var contentSecurityPolicy = BuildContentSecurityPolicy(cspOptions);
        context.Response.Headers["Content-Security-Policy"] = contentSecurityPolicy;
        context.Response.Headers["Permissions-Policy"] = "xr-spatial-tracking=()";
    }

    await next();
});
//Dung de tai file trong wwwroot
app.UseStaticFiles();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

static string BuildContentSecurityPolicy(ContentSecurityPolicyOptions options)
{
    static string JoinDirective(IEnumerable<string> values, IEnumerable<string>? extraValues = null)
    {
        var merged = extraValues is null
            ? values
            : values.Concat(extraValues);

        return string.Join(" ", merged.Where(value => !string.IsNullOrWhiteSpace(value)).Distinct(StringComparer.OrdinalIgnoreCase));
    }

    var trustedOrigins = options.TrustedOrigins
        .Where(value => !string.IsNullOrWhiteSpace(value))
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToArray();

    var directives = new List<string>
    {
        $"default-src {JoinDirective(options.DefaultSrc)}",
        $"base-uri {JoinDirective(options.BaseUri)}",
        $"object-src {JoinDirective(options.ObjectSrc)}",
        $"frame-ancestors {JoinDirective(options.FrameAncestors)}",
        $"form-action {JoinDirective(options.FormAction, trustedOrigins)}",
        $"img-src {JoinDirective(options.ImgSrc)}",
        $"font-src {JoinDirective(options.FontSrc)}",
        $"style-src {JoinDirective(options.StyleSrc)}",
        $"script-src {JoinDirective(options.ScriptSrc)}",
        $"connect-src {JoinDirective(options.ConnectSrc, trustedOrigins)}",
        $"frame-src {JoinDirective(options.FrameSrc, trustedOrigins)}",
        $"worker-src {JoinDirective(options.WorkerSrc)}"
    };

    if (options.UpgradeInsecureRequests)
    {
        directives.Add("upgrade-insecure-requests");
    }

    return string.Join("; ", directives);
}

