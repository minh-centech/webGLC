using System.Web.Mvc;

namespace webGLC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "KiemTraHangNhap",
               "quan-tri/kiem-tra-hang-nhap",
               new { controller = "KiemTraHangNhap", action = "Index", id = UrlParameter.Optional }

           );
            context.MapRoute(
                "LenhOnline",
                "quan-tri/lenh",
                new { controller = "LenhOnline", action = "Index", id = UrlParameter.Optional }

            );
            context.MapRoute(
                "DangKy",
                "quan-tri/dang-ky",
                new { controller = "DangKy", action = "Index", id = UrlParameter.Optional }

            );
            context.MapRoute(
                "HuongDan",
                "quan-tri/huong-dan",
                new { controller = "HuongDan", action = "Index", id = UrlParameter.Optional }
          
            );
            context.MapRoute(
              "TrangChu",
              "quan-tri",
              new { action = "Index", controller = "Home", id = UrlParameter.Optional }
          );
            context.MapRoute(
               "Admin_default",
               "Admin/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );
            // context.MapRoute(
            //    "HuongDan",
            //    "Admin/huong-dan",
            //    new { controller = "HuongDan", action = "Index", id = UrlParameter.Optional }
            //     //namespaces: new[] { "webGLC.Areas.Admin.Controllers" }
            //);

        }
    }
}