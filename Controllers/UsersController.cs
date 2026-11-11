using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewTask.Models;
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
                using var reader = new StreamReader(Request.Body);
                string body = await reader.ReadToEndAsync();
                if (body.Contains("df2323eoT")) secretParam = "df2323eoT";
            }

            if (secretParam != "df2323eoT") return Unauthorized();

            return View(DataStore.Users);
        }

        [HttpGet("create")]
        public IActionResult Create([FromQuery] string username, [FromQuery] string email, [FromQuery] string role)
        {
            var user = new User { Username = username, Email = email, Role = role };
            return View(user);
        }

        [HttpPost("create")]
        public IActionResult Create(User user)
        {
            user.Id = DataStore.Users.Any() ? DataStore.Users.Max(u => u.Id) + 1 : 1;
            DataStore.Users.Add(user);
            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        [HttpGet("details/{id:int}")]
        public IActionResult Details(int id)
        {
            var user = DataStore.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }
    }
}