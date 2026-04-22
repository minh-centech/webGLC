using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using webAPI.Code;

namespace webAPI.Controllers
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        [Route("api/healthcheck/database")]
        public IHttpActionResult Database()
        {
            string connectionString = GlobalVariables.ConnectionString;
            var watch = Stopwatch.StartNew();

            string maskedServer = "Unknown";
            string dbName = "Unknown";

            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);

                string fullServer = builder.DataSource;
                dbName = builder.InitialCatalog;

                // Ẩn port, chỉ lấy tên server/IP
                maskedServer = fullServer.Split(',', ':')[0].Trim();

                // Ép timeout kết nối về 5 giây nếu chưa có hoặc đang quá lớn
                if (builder.ConnectTimeout <= 0 || builder.ConnectTimeout > 5)
                {
                    builder.ConnectTimeout = 5;
                }

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    // Timeout cho câu lệnh test DB
                    using (SqlCommand cmd = new SqlCommand("SELECT 1", connection))
                    {
                        cmd.CommandTimeout = 5; // 5 giây
                        cmd.ExecuteScalar();
                    }

                    watch.Stop();

                    return Ok(new
                    {
                        status = "Healthy",
                        connectedTo = new
                        {
                            serverIp = maskedServer,
                            databaseName = dbName
                        },
                        responseTimeMs = watch.ElapsedMilliseconds,
                        timestamp = DateTime.UtcNow
                    });
                }
            }
            catch (SqlException ex)
            {
                watch.Stop();

                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    status = "Unhealthy",
                    message = "Không thể kết nối tới cơ sở dữ liệu.",
                    connectedTo = new
                    {
                        serverIp = maskedServer,
                        databaseName = dbName
                    },
                    responseTimeMs = watch.ElapsedMilliseconds,
                    timestamp = DateTime.UtcNow,
                    errorType = "SqlException",
                    errorCode = ex.Number
                });
            }
            catch (TimeoutException)
            {
                watch.Stop();

                return Content(HttpStatusCode.ServiceUnavailable, new
                {
                    status = "Unhealthy",
                    message = "Kết nối cơ sở dữ liệu bị timeout.",
                    connectedTo = new
                    {
                        serverIp = maskedServer,
                        databaseName = dbName
                    },
                    responseTimeMs = watch.ElapsedMilliseconds,
                    timestamp = DateTime.UtcNow,
                    errorType = "TimeoutException"
                });
            }
            catch (Exception)
            {
                watch.Stop();

                return Content(HttpStatusCode.InternalServerError, new
                {
                    status = "Unhealthy",
                    message = "Lỗi khi kiểm tra kết nối cơ sở dữ liệu.",
                    connectedTo = new
                    {
                        serverIp = maskedServer,
                        databaseName = dbName
                    },
                    responseTimeMs = watch.ElapsedMilliseconds,
                    timestamp = DateTime.UtcNow,
                    errorType = "Exception"
                });
            }
        }



        // Route: GET api/healthcheck/ping
        [HttpGet]
        [Route("api/healthcheck/ping")]
        public IHttpActionResult Ping()
        {
            return Ok(new { message = "Pong!", serverTime = DateTime.Now });
        }
    }
}