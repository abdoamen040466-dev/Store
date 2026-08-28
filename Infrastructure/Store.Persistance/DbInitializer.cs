using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Persistance.Data.Contexts;
using System.Text.Json;

namespace Store.Persistance;

public class DbInitializer(StoreDbContext _context) : IDbInitializer
{


    public async Task InitializeAsync()
    {
        // Create DB
        // Update DB
        if ((await _context.Database.GetPendingMigrationsAsync()).Any())
        {
            await _context.Database.MigrateAsync();
        }

        // Data seeding
        // Product Brands
        if (!_context.ProductBrands.Any())
        {
            // 1. Read All Data from JSON file 'brand.json'
            // D:\backend\08 Asp.Net Core Web Apis\Session 02\New folder\Store\Infrastructure\Store.Persistance\Data\DataSeeding\brands.json
            var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistance\Data\DataSeeding\brands.json");



            // 2. Convert the JsonString to List<ProductBrand>
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            if (brands is not null && brands.Count > 0)
            {
                await _context.ProductBrands.AddRangeAsync(brands);
            }
        }


        // Product Types
        if (!_context.ProductTypes.Any())
        {
            // 1. Read All Data from JSON file 'types.json'
            // D:\backend\08 Asp.Net Core Web Apis\Session 02\New folder\Store\Infrastructure\Store.Persistance\Data\DataSeeding\types.json
            var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistance\Data\DataSeeding\types.json");


            // 2. Convert the JsonString to List<ProductBrand>
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

            if (types is not null && types.Count > 0)
            {
                await _context.ProductTypes.AddRangeAsync(types);
            }
        }


        // Products
        if (!_context.Products.Any())
        {
            // 1. Read All Data from JSON file 'products.json'
            // D:\backend\08 Asp.Net Core Web Apis\Session 02\New folder\Store\Infrastructure\Store.Persistance\Data\DataSeeding\products.json
            var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistance\Data\DataSeeding\products.json");


            // 2. Convert the JsonString to List<ProductBrand>
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products is not null && products.Count > 0)
            {
                await _context.Products.AddRangeAsync(products);
            }
        }


        await _context.SaveChangesAsync();

    }
}
