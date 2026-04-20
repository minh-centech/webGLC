using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using webGLCv2.Components;
using webGLCv2.Controllers;
using webGLCv2.Models;
using webGLCv2.Services;

var builder = WebApplication.CreateBuilder(args);
var legacyApiSection = builder.Configuration.GetSection(LegacyApiOptions.SectionName);
var turnstileSection = builder.Configuration.GetSection(TurnstileOptions.SectionName);

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
builder.Services.Configure<TurnstileOptions>(turnstileSection);

builder.Services.AddHttpClient("LegacyApi", (serviceProvider, client) =>
{
    var options = legacyApiSection.Get<LegacyApiOptions>() ?? new LegacyApiOptions();

    if (string.IsNullOrWhiteSpace(options.BaseUrl))
    {
        throw new InvalidOperationException("Missing configuration: LegacyApi:BaseUrl");
    }

    client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<LegacyCustomerPortalService>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    return new LegacyCustomerPortalService(httpClientFactory.CreateClient("LegacyApi"));
});

builder.Services.AddScoped<OnlineOrderService>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    return new OnlineOrderService(httpClientFactory.CreateClient("LegacyApi"));
});

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
app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
