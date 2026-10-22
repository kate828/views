using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using ViewTask.Models;
using ViewTask.Services;

namespace ViewTask.Controllers
{
    [Route("products")]
    [Route("items")]
    public class ProductsController : Controller
    {
        // 1. Список продуктів + фільтрація з Query String (?name=Bread&minPrice=5&maxPrice=100)
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index([FromQuery] string? name, [FromQuery] double? minPrice, [FromQuery] double? maxPrice)
        {
            var query = DataStore.Products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return View(query.ToList());
        }

        // 2. Деталі про продукт: /products/id
        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            return View(product);
        }

        // 3. Створення продукту: /products/create та /products/new
        [HttpGet("create")]
        [HttpGet("new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [HttpPost("new")]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = DataStore.Products.Any() ? DataStore.Products.Max(p => p.Id) + 1 : 1;
                DataStore.Products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // 4. Редагування: /products/edit/id
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            return View(product);
        }

        [HttpPost("edit/{id:int}")]
        public IActionResult Edit(Product product)
        {
            var existing = DataStore.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            if (ModelState.IsValid)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // 5. Видалення: /products/delete/id
        [HttpGet("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            return View(product);
        }

        [HttpPost("delete/{id:int}")]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            DataStore.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        // Кастомна сторінка 404
        [HttpGet("notfound")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}