using System.Collections.Generic;
using ViewTask.Models;

namespace ViewTask.Services
{
    public static class DataStore
    {
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Bear", Price = 150, Type = ProductType.Toy, Description = "Bear is fluffy" },
            new Product { Id = 2, Name = "Phone", Price = 12000, Type = ProductType.Technique, Description = "Phone smartphone" },
            new Product { Id = 3, Name = "Shirt", Price = 500, Type = ProductType.Clothes },
            new Product { Id = 4, Name = "Bike", Price = 8000, Type = ProductType.Transport }
        };

        public static List<User> Users { get; set; } = new List<User>
        {
            new User { Id = 1, Username = "admin", Email = "admin@test.com", Role = "Admin" }
        };
    }
}