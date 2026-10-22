using System.Collections.Generic;
using ViewTask.Models;

namespace ViewTask.Services
{
    public static class DataStore
    {
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Bread", Price = 10 },
            new Product { Id = 2, Name = "Milk", Price = 11 },
            new Product { Id = 3, Name = "Cheese", Price = 140 },
            new Product { Id = 4, Name = "Sausage", Price = 110 },
            new Product { Id = 5, Name = "Potato", Price = 7 }
        };

        public static List<User> Users { get; set; } = new List<User>
        {
            new User { Id = 1, Username = "admin", Email = "admin@test.com", Role = "Admin" },
            new User { Id = 2, Username = "john_doe", Email = "john@test.com", Role = "User" },
            new User { Id = 3, Username = "kate_p", Email = "kate@test.com", Role = "User" }
        };
    }
}