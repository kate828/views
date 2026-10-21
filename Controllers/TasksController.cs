using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ViewTask.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult SprintTasks() => View();
        
        public IActionResult Greetings() => View();
        
        public IActionResult ProductInfo() => View();

        public IActionResult SuperMarkets()
        {
            ViewBag.SuperMarkets = new List<string> { "WellMart", "Silpo", "ATB", "Furshet", "Metro" };
            return View();
        }

        public IActionResult ShoppingList()
        {
            var products = new Dictionary<string, int>
            {
                { "Milk", 2 }, { "Bread", 2 }, { "Cake", 1 }, { "Ice Cream", 5 }, { "Cola", 10 }
            };
            return View(products);
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            ViewBag.Markets = new List<string> { "WellMart", "Silpo", "ATB" };
            ViewBag.Products = new List<string> { "Bread", "Milk", "Cheese", "Sausage" };
            return View();
        }

        [HttpPost]
        public IActionResult ShoppingCart(string fullname, string address)
        {
            return Content($"Your products will be shipped at: {address}. Bon appetite, {fullname}!");
        }
    }
}