using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewTask.Services;

namespace ViewTask.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        [HttpGet("index")]
        [HttpPost("index")]
        public async Task<IActionResult> Index()
        {
            string? secretParam = null;

            // Перевірка даних у тілі запиту (Form Body)
            if (Request.HasFormContentType && Request.Form.Count > 0)
            {
                foreach (var key in Request.Form.Keys)
                {
                    if (Request.Form[key] == "df2323eoT" || key == "df2323eoT")
                    {
                        secretParam = "df2323eoT";
                        break;
                    }
                }
            }
            else if (Request.Body.CanRead)
            {
                // Перевірка сирого тіла запиту (Raw Body)
                using var reader = new StreamReader(Request.Body);
                string body = await reader.ReadToEndAsync();
                if (body.Contains("df2323eoT"))
                {
                    secretParam = "df2323eoT";
                }
            }

            // Якщо параметра в тілі запиту немає — повертаємо 401 Unauthorized
            if (secretParam != "df2323eoT")
            {
                return Unauthorized();
            }

            return View(DataStore.Users);
        }
    }
}