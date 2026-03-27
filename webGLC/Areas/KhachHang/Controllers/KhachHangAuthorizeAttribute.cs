using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace webGLC.Areas.KhachHang.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class KhachHangAuthorizeAttribute : AuthorizeAttribute
    {
        private const string SessionUserEmailKey = "KhachHangEmail";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }

            var isAuthenticated = httpContext.User != null && httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated;
            var hasSessionEmail = httpContext.Session != null && httpContext.Session[SessionUserEmailKey] != null;

            return isAuthenticated && hasSessionEmail;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            FormsAuthentication.SignOut();

            if (filterContext.HttpContext.Session != null)
            {
                filterContext.HttpContext.Session.Clear();
                filterContext.HttpContext.Session.Abandon();
            }

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "area", "KhachHang" },
                    { "controller", "Login" },
                    { "action", "Index" }
                });
        }
    }
}
