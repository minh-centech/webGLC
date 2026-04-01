using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webGLCv2.Models;
using webGLCv2.Services;

namespace webGLCv2.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public sealed class AuthController : Controller
{
    private readonly LegacyCustomerPortalService _legacyCustomerPortalService;

    public AuthController(LegacyCustomerPortalService legacyCustomerPortalService)
    {
        _legacyCustomerPortalService = legacyCustomerPortalService;
    }

    [AllowAnonymous]
    [HttpPost("/auth/login")]
    public async Task<IActionResult> Login([FromForm] LoginPostModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToLogin(model.ReturnUrl, "Vui lòng nhập đầy đủ thông tin đăng nhập.", model.Email);
        }

        try
        {
            var loginResult = await _legacyCustomerPortalService.LoginAsync(
                model.Email.Trim(),
                model.Password,
                model.CaptchaCode.Trim().ToUpperInvariant(),
                model.CaptchaToken);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, loginResult.Email),
                new(ClaimTypes.Email, loginResult.Email),
                new(ClaimTypes.NameIdentifier, loginResult.Id),
                new("display_name", loginResult.DisplayName),
                new("account_type", loginResult.AccountType.ToString()),
                new("account_type_name", loginResult.AccountTypeName),
                new(ClaimTypes.Role, loginResult.RoleName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                });

            var returnUrl = string.IsNullOrWhiteSpace(model.ReturnUrl) ? GetDefaultRoute(loginResult.RoleName) : model.ReturnUrl;
            if (!Url.IsLocalUrl(returnUrl))
            {
                returnUrl = GetDefaultRoute(loginResult.RoleName);
            }

            return LocalRedirect(returnUrl);
        }
        catch (Exception ex)
        {
            return RedirectToLogin(model.ReturnUrl, ex.Message, model.Email);
        }
    }

    [Authorize]
    [HttpGet("/auth/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/login");
    }

    private IActionResult RedirectToLogin(string? returnUrl, string error, string? email)
    {
        var query = new List<string> { $"error={Uri.EscapeDataString(error)}" };

        if (!string.IsNullOrWhiteSpace(returnUrl))
        {
            query.Add($"returnUrl={Uri.EscapeDataString(returnUrl)}");
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            query.Add($"email={Uri.EscapeDataString(email)}");
        }

        return Redirect("/login?" + string.Join("&", query));
    }

    private static string GetDefaultRoute(string roleName)
        => string.Equals(roleName, "Admin", StringComparison.OrdinalIgnoreCase)
            ? "/admin/users"
            : "/user/orders";
}
