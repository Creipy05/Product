using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductBackend.Models;

namespace ProductBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpPost("RecreateDatabase")]
    public async Task RecreateDatabase()
    {
        using var db = new ProductContext();
        await db.Database.EnsureDeletedAsync();
        await db.Database.MigrateAsync();
    }

    [HttpPost("Populate")]
    public async Task Populate()
    {
        var products = new List<Product>();
        var product1 = new Product() { Id = Guid.NewGuid(), Name = "Alma", Price = 123.4, Rating = 3, Active = true, };
        products.Add(product1);
        var product2 = new Product() { Id = Guid.NewGuid(), Name = "Körte", Price = 324523.4, Rating = 2, Active = true, };
        products.Add(product2);
        var product3 = new Product() { Id = Guid.NewGuid(), Name = "Barack", Price = 3434, Rating = 1, Active = true, };
        products.Add(product3);

        using var db = new ProductContext();
        db.Products.AddRange(products);

        var product4 = new Product() { Id = Guid.NewGuid(), Name = "Szõlõ", Price = 3423, Rating = 2, Active = false, };
        db.Products.Add(product4);
        await db.SaveChangesAsync();

    }
    [HttpGet]
    public async Task<List<Product>> Get(string? search)
    {
        using var db = new ProductContext();

        var query = db.Products
            .Where(x => x.Active);
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query
                .Where(x => x.Name.ToLower().Contains(search.ToLower()));

        }
        query = query
            .OrderByDescending(x => x.Rating);
        var res = query.ToList();


        //var res = db.Products
        //    .Where(x => x.Name.ToLower().Contains(search.ToLower()))
        //    .Where(x => x.Active)
        //    .OrderByDescending(x => x.Rating)
        //    .ToList();
        await Task.Delay(500);
        return res;
    }
    [HttpGet("Get2")]
    public List<Product> Get2(string search)
    {
        var res = new List<Product>();
        var product1 = new Product() { Id = Guid.NewGuid(), Name = "Alma", Price = 123.4, Rating = 3, Active = true, };
        res.Add(product1);
        var product2 = new Product() { Id = Guid.NewGuid(), Name = "Körte", Price = 324523.4, Rating = 2, Active = true, };
        res.Add(product2);
        var product3 = new Product() { Id = Guid.NewGuid(), Name = "Barack", Price = 3434, Rating = 1, Active = true, };
        res.Add(product3);

        var res2 = res
            .Where(x => x.Name.ToLower().Contains(search.ToLower()))
            .Where(x => x.Active)
            .OrderByDescending(x => x.Rating)
            .ToList();

        return res2;
    }
}
