using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewTask.Models;
using ViewTask.Services;

namespace ViewTask.Controllers
{
    [Route("products")]
    [Route("items")]
    public class ProductsController : Controller
    {
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index([FromQuery] string? name, [FromQuery] double? minPrice, [FromQuery] double? maxPrice)
        {
            var query = DataStore.Products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
            
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            return View(query.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction(nameof(NotFoundPage));
            return View(product);
        }

        [HttpGet("create")]
        [HttpGet("new")]
        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost("create")]
        [HttpPost("new")]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = DataStore.Products.Any() ? DataStore.Products.Max(p => p.Id) + 1 : 1;
                DataStore.Products.Add(product);
                return RedirectToAction(nameof(Details), new { id = product.Id });
            }
            return View(product);
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction(nameof(NotFoundPage));
            return View(product);
        }

        [HttpPost("edit/{id:int}")]
        public IActionResult Edit(Product product)
        {
            var existing = DataStore.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null) return RedirectToAction(nameof(NotFoundPage));

            if (ModelState.IsValid)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Description = product.Description;
                existing.Type = product.Type;
                return RedirectToAction(nameof(Details), new { id = existing.Id });
            }
            return View(product);
        }

        [HttpGet("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction(nameof(NotFoundPage));
            return View(product);
        }

        [HttpPost("delete/{id:int}")]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return RedirectToAction(nameof(NotFoundPage));

            DataStore.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("bulkedit/{type}")]
        public IActionResult BulkEdit(ProductType type)
        {
            var products = DataStore.Products.Where(p => p.Type == type).ToList();
            ViewBag.SelectedType = type;
            return View(products);
        }

        [HttpPost("bulkedit/{type}")]
        public IActionResult BulkEdit(ProductType type, List<Product> products)
        {
            if (ModelState.IsValid)
            {
                foreach (var p in products)
                {
                    var existing = DataStore.Products.FirstOrDefault(x => x.Id == p.Id);
                    if (existing != null) existing.Price = p.Price;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SelectedType = type;
            return View(products);
        }

        [HttpGet("notfound")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}