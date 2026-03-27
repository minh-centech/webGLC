using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webGLC.Areas.KhachHang.Controllers
{
    [KhachHangAuthorize]
    public class NguoiDungController : Controller
    {
        // GET: Admin/NguoiDung
        public ActionResult Index()
        {
            return View();
        }
    }
}
