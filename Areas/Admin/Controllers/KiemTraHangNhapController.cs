using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webGLC.Areas.Admin.Controllers
{
    public class KiemTraHangNhapController : Controller
    {
        // GET: Admin/KiemTraHangNhap
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            return View();
        }
    }
}