using System.Web.Http;

namespace webAPI.Controllers
{
    public class StatusController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok(new
            {
                message = "webAPI is running",
                endpoints = new[]
                {
                    "/api/DanhMucKhachHangDoiLenh/List",
                    "/api/DanhMucKhachHangDoiLenh/Login",
                    "/api/DanhMucKhachHangDoiLenh/Insert"
                }
            });
        }
    }
}
