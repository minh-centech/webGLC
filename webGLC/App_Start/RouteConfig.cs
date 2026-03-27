using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webGLC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var loginRoute = routes.MapRoute(
                name: "RootLogin",
                url: "",
                defaults: new { controller = "Login", action = "Index" },
                namespaces: new[] { "webGLC.Areas.KhachHang.Controllers" }
            );
            loginRoute.DataTokens = new RouteValueDictionary(new { area = "KhachHang" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webGLC.Controllers" }
               );
          
          
        }
    }
}
