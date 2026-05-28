using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class EmailHelper
{
    public const string RegistrationSuccessTemplateId = "registration-success";
    public const string RegistrationPendingApprovalTemplateId = "registration-pending-approval";
    public const string RegistrationPendingApprovalAdminNotificationTemplateId = "registration-pending-approval-admin-notification";
    public const string PasswordResetSuccessTemplateId = "password-reset-success";
    public const string PasswordResetGeneratedTemplateId = "password-reset-generated";
    public const string AccountApprovedSuccessTemplateId = "account-approved-success";

    private readonly EmailSenderOptions _options;
    private readonly ILogger<EmailHelper> _logger;

    public EmailHelper(IOptions<EmailSenderOptions> options, ILogger<EmailHelper> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string templateId)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("toEmail is required.", nameof(toEmail));
        }

        var normalizedToEmail = toEmail.Trim();
        _logger.LogInformation(
            "EmailHelper.SendEmailAsync start. TemplateId={TemplateId}, ToEmail={ToEmail}, SmtpHost={SmtpHost}, SmtpPort={SmtpPort}, UseSsl={UseSsl}",
            templateId,
            normalizedToEmail,
            _options.Host,
            _options.Port,
            _options.SSL);

        var template = ResolveTemplate(templateId);
        _logger.LogDebug(
            "EmailHelper.SendEmailAsync template resolved. Subject={Subject}, BodyLength={BodyLength}",
            template.Subject,
            template.Body?.Length ?? 0);

        using var message = new MailMessage
        {
            From = new MailAddress(_options.Username, _options.SenderName),
            Subject = template.Subject,
            Body = template.Body,
            IsBodyHtml = true
        };

        message.To.Add(normalizedToEmail);
        _logger.LogDebug(
            "EmailHelper.SendEmailAsync message prepared. From={From}, To={To}, Subject={Subject}",
            _options.Username,
            normalizedToEmail,
            template.Subject);

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.SSL,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };

        try
        {
            _logger.LogInformation("EmailHelper.SendEmailAsync calling SMTP server.");
            await client.SendMailAsync(message);
            _logger.LogInformation("EmailHelper.SendEmailAsync success. ToEmail={ToEmail}, TemplateId={TemplateId}", normalizedToEmail, templateId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "EmailHelper.SendEmailAsync failed. ToEmail={ToEmail}, TemplateId={TemplateId}", normalizedToEmail, templateId);
            throw;
        }
    }

    public async Task SendPasswordResetEmailAsync(string toEmail, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("toEmail is required.", nameof(toEmail));
        }

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            throw new ArgumentException("newPassword is required.", nameof(newPassword));
        }

        var normalizedToEmail = toEmail.Trim();
        var normalizedPassword = newPassword.Trim();

        _logger.LogInformation(
            "EmailHelper.SendPasswordResetEmailAsync start. ToEmail={ToEmail}, PasswordLength={PasswordLength}, SmtpHost={SmtpHost}, SmtpPort={SmtpPort}, UseSsl={UseSsl}",
            normalizedToEmail,
            normalizedPassword.Length,
            _options.Host,
            _options.Port,
            _options.SSL);

        using var message = new MailMessage
        {
            From = new MailAddress(_options.Username, _options.SenderName),
            Subject = "Mật khẩu mới của bạn",
            Body = $"<p>Hệ thống đã tạo mật khẩu mới cho tài khoản của bạn.</p><p><strong>Mật khẩu mới:</strong> {normalizedPassword}</p><p>Vui lòng đăng nhập và thay đổi mật khẩu ngay sau khi truy cập.</p>",
            IsBodyHtml = true
        };

        message.To.Add(normalizedToEmail);
        _logger.LogDebug(
            "EmailHelper.SendPasswordResetEmailAsync message prepared. From={From}, To={To}",
            _options.Username,
            normalizedToEmail);

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.SSL,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };

        try
        {
            _logger.LogInformation("EmailHelper.SendPasswordResetEmailAsync calling SMTP server.");
            await client.SendMailAsync(message);
            _logger.LogInformation("EmailHelper.SendPasswordResetEmailAsync success. ToEmail={ToEmail}", normalizedToEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "EmailHelper.SendPasswordResetEmailAsync failed. ToEmail={ToEmail}", normalizedToEmail);
            throw;
        }
    }

    public async Task SendRegistrationPendingApprovalNotificationAsync(RegisterAccountModel model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var recipients = _options.RegistrationPendingApprovalNotificationEmails
            .Where(email => !string.IsNullOrWhiteSpace(email))
            .Select(email => email.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (recipients.Count == 0)
        {
            _logger.LogWarning(
                "EmailHelper.SendRegistrationPendingApprovalNotificationAsync skipped because no admin recipients are configured.");
            return;
        }

        var subject = "Thông báo: Có người dùng đăng ký tài khoản chờ xác nhận";
        var body = $@"<p>Có một yêu cầu đăng ký tài khoản mới trên hệ thống <strong>everWareHouse GLC</strong>.</p>
<p><strong>Họ và tên:</strong> {WebUtility.HtmlEncode(model.Ten)}</p>
<p><strong>Email:</strong> {WebUtility.HtmlEncode(model.Email)}</p>
<p><strong>Số điện thoại:</strong> {WebUtility.HtmlEncode(model.SoDienThoai)}</p>
<p><strong>Loại tài khoản:</strong> {model.LoaiTaiKhoan}</p>
<p><strong>Tên đăng nhập:</strong> {WebUtility.HtmlEncode(model.TenDangNhap ?? model.Email)}</p>
<p>Vui lòng đăng nhập hệ thống để xác nhận hoặc từ chối yêu cầu này.</p>";

        foreach (var recipient in recipients)
        {
            await SendRawEmailAsync(recipient, subject, body, RegistrationPendingApprovalAdminNotificationTemplateId);
        }
    }

    private static EmailTemplate ResolveTemplate(string templateId)
        => templateId switch
        {
            RegistrationSuccessTemplateId => new EmailTemplate(
                "Đăng ký tài khoản thành công",
                @"<p>Chúc mừng bạn đã đăng ký tài khoản thành công trên hệ thống <strong>everWareHouse</strong>.</p>
                <p>Hiện tại, hồ sơ đăng ký của bạn đã được chuyển đến bộ phận quản lý để tiến hành xét duyệt theo quy định.</p>
                <p>Hệ thống sẽ sớm phản hồi và thông báo kết quả chi tiết trực tiếp qua địa chỉ email này của bạn.</p>
                <p>Trân trọng,<br /><strong>Ban quản trị cổng everWareHouse</strong></p>"),
            RegistrationPendingApprovalTemplateId => new EmailTemplate(
                "Tài khoản đã được tạo và đang chờ phê duyệt",
                @"<p>Yêu cầu tạo tài khoản của bạn tại <strong>everWareHouse</strong> đã được tiếp nhận thành công.</p>
                <p>Hiện tại, tài khoản đang chờ quản trị viên phê duyệt để chính thức kích hoạt các tính năng trên hệ thống.</p>
                <p>Kết quả xét duyệt hồ sơ sẽ được cập nhật và gửi thông báo đến bạn trong thời gian sớm nhất.</p>"),
            PasswordResetSuccessTemplateId => new EmailTemplate(
                "Lấy lại mật khẩu thành công",
                "<p>Yêu cầu lấy lại mật khẩu của bạn đã được xử lý thành công. Bạn có thể đăng nhập lại bằng mật khẩu mới.</p>"),
            AccountApprovedSuccessTemplateId => new EmailTemplate(
                "Tài khoản đã được phê duyệt",
                @"<p>Xin chúc mừng!</p>
                <p>Hồ sơ đăng ký của bạn trên hệ thống <strong>everWareHouse</strong> đã được phê duyệt thành công.</p>
                <p>Hiện tại, tài khoản của bạn đã được kích hoạt đầy đủ các tính năng. Bạn có thể đăng nhập vào hệ thống ngay bây giờ để bắt đầu sử dụng dịch vụ.</p>
                <p>Trân trọng,<br />
                <strong>Ban quản trị cổng everWareHouse</strong></p>"),
            _ => throw new ArgumentOutOfRangeException(nameof(templateId), templateId, "Mẫu email không hợp lệ.")
        };

    private async Task SendRawEmailAsync(string toEmail, string subject, string body, string templateId)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("toEmail is required.", nameof(toEmail));
        }

        var normalizedToEmail = toEmail.Trim();
        _logger.LogInformation(
            "EmailHelper.SendRawEmailAsync start. TemplateId={TemplateId}, ToEmail={ToEmail}, Subject={Subject}, SmtpHost={SmtpHost}, SmtpPort={SmtpPort}, UseSsl={UseSsl}",
            templateId,
            normalizedToEmail,
            subject,
            _options.Host,
            _options.Port,
            _options.SSL);

        using var message = new MailMessage
        {
            From = new MailAddress(_options.Username, _options.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(normalizedToEmail);

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.SSL,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };

        try
        {
            await client.SendMailAsync(message);
            _logger.LogInformation("EmailHelper.SendRawEmailAsync success. ToEmail={ToEmail}, TemplateId={TemplateId}", normalizedToEmail, templateId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "EmailHelper.SendRawEmailAsync failed. ToEmail={ToEmail}, TemplateId={TemplateId}", normalizedToEmail, templateId);
            throw;
        }
    }

    private sealed record EmailTemplate(string Subject, string Body);
}
