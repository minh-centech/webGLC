using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webGLC.Areas.Admin.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Chưa nhập Tài khoản")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Chưa nhập Mật khẩu")]
        public string MatKhau { get; set; }
    }
}