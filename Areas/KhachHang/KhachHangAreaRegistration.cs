using System.Web.Mvc;

namespace webGLC.Areas.KhachHang
{
    public class KhachHangAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "KhachHang";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "KiemTraHangNhap",
               "khach-hang/kiem-tra-hang-nhap",
               new { controller = "KiemTraHangNhap", action = "Index", id = UrlParameter.Optional }

           );
            context.MapRoute(
                "LenhOnline",
                "khach-hang/lenh",
                new { controller = "LenhOnline", action = "Index", id = UrlParameter.Optional }

            );
            context.MapRoute(
                "DangKy",
                "khach-hang/dang-ky",
                new { controller = "DangKy", action = "Index", id = UrlParameter.Optional }

            );
            context.MapRoute(
                "HuongDan",
                "khach-hang/huong-dan",
                new { controller = "HuongDan", action = "Index", id = UrlParameter.Optional }
          
            );
            context.MapRoute(
              "TrangChu",
              "khach-hang",
              new { action = "Index", controller = "Home", id = UrlParameter.Optional }
          );
            context.MapRoute(
               "KhachHang_default",
               "KhachHang/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );
            // Add other routes similarly
           
            // context.MapRoute(
            //    "HuongDan",
            //    "KhachHang/huong-dan",
            //    new { controller = "HuongDan", action = "Index", id = UrlParameter.Optional }
            //     //namespaces: new[] { "webGLC.Areas.KhachHang.Controllers" }
            //);

        }
    }
}