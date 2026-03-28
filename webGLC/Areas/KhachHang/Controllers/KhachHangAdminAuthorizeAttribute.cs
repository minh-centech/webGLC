using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webGLC.Areas.KhachHang.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class KhachHangAdminAuthorizeAttribute : KhachHangAuthorizeAttribute
    {
        private const string SessionUserAccountTypeKey = "KhachHangLoaiTaiKhoan";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }

            var accountType = httpContext.Session?[SessionUserAccountTypeKey]?.ToString();
            return string.Equals(accountType, "0", StringComparison.OrdinalIgnoreCase);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext?.HttpContext?.User?.Identity != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "area", "KhachHang" },
                        { "controller", "Home" },
                        { "action", "Index" }
                    });
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
