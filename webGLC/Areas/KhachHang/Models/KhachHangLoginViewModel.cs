using System.ComponentModel.DataAnnotations;

namespace webGLC.Areas.KhachHang.Models
{
    public class KhachHangLoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email đăng nhập.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã captcha.")]
        [Display(Name = "Mã captcha")]
        public string CaptchaCode { get; set; }

        public string CaptchaToken { get; set; }

        public string CaptchaDisplayText { get; set; }
    }
}
