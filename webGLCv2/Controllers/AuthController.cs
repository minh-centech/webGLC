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
    private readonly TurnstileValidationService _turnstileValidationService;

    public AuthController(
        LegacyCustomerPortalService legacyCustomerPortalService,
        TurnstileValidationService turnstileValidationService)
    {
        _legacyCustomerPortalService = legacyCustomerPortalService;
        _turnstileValidationService = turnstileValidationService;
    }

    [AllowAnonymous]
    [HttpPost("/auth/login")]
    public async Task<IActionResult> Login(
        [FromForm] LoginPostModel model,
        [FromForm(Name = "cf-turnstile-response")] string turnstileToken)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(turnstileToken))
        {
            return RedirectToLogin(model.ReturnUrl, "Để truy cập hệ thống, vui lòng nhập email, mật khẩu và mã xác thực bảo mật của bạn.", model.Email);
        }

        try
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var turnstileResult = await _turnstileValidationService.ValidateDetailedAsync(
                turnstileToken,
                remoteIp,
                HttpContext.RequestAborted);

            if (!turnstileResult.Success)
            {
                var errorDetail = string.IsNullOrWhiteSpace(turnstileResult.ErrorDetail)
                    ? "unknown-turnstile-error"
                    : turnstileResult.ErrorDetail;
                return RedirectToLogin(model.ReturnUrl, $"Xac thuc Turnstile khong thanh cong. Vui long thu lai. ({errorDetail})", model.Email);
            }

            var loginResult = await _legacyCustomerPortalService.LoginAsync(
                model.Email.Trim(),
                model.Password);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, loginResult.Email),
                new(ClaimTypes.Email, loginResult.Email),
                new(ClaimTypes.NameIdentifier, loginResult.Id),
                new("display_name", loginResult.DisplayName),
                new("id_khachhang_doilenh", loginResult.Id),
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


