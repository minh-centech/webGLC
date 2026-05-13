using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class EmailHelper
{
    public const string RegistrationSuccessTemplateId = "registration-success";
    public const string RegistrationPendingApprovalTemplateId = "registration-pending-approval";
    public const string PasswordResetSuccessTemplateId = "password-reset-success";
    public const string PasswordResetGeneratedTemplateId = "password-reset-generated";
    public const string AccountApprovedSuccessTemplateId = "account-approved-success";

    private readonly EmailSenderOptions _options;

    public EmailHelper(IOptions<EmailSenderOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(string toEmail, string templateId)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("toEmail is required.", nameof(toEmail));
        }

        var template = ResolveTemplate(templateId);
        using var message = new MailMessage
        {
            From = new MailAddress(_options.Username, _options.EmailSender),
            Subject = template.Subject,
            Body = template.Body,
            IsBodyHtml = true
        };

        message.To.Add(toEmail.Trim());

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.SSL,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };

        await client.SendMailAsync(message);
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

        using var message = new MailMessage
        {
            From = new MailAddress(_options.Username, _options.EmailSender),
            Subject = "Mật khẩu mới của bạn",
            Body = $"<p>Hệ thống đã tạo mật khẩu mới cho tài khoản của bạn.</p><p><strong>Mật khẩu mới:</strong> {newPassword.Trim()}</p><p>Vui lòng đăng nhập và thay đổi mật khẩu ngay sau khi truy cập.</p>",
            IsBodyHtml = true
        };

        message.To.Add(toEmail.Trim());

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.SSL,
            Credentials = new NetworkCredential(_options.Username, _options.Password)
        };

        await client.SendMailAsync(message);
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

    private sealed record EmailTemplate(string Subject, string Body);
}
