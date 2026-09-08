using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Domain.Entities.Orders;
using Store.Domain.Entities.Products;
using Store.Persistance.Data.Contexts;
using Store.Persistance.Identity.Contexts;
using System.Text.Json;

namespace Store.Persistance;

public class DbInitializer(
    StoreDbContext _context,
    IdentityStoreDbContext _identityContext,
    UserManager<AppUser> _userManager,
    RoleManager<IdentityRole> _roleManager
    ) : IDbInitializer
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

        // Order 
        if (!_context.DeliveryMethods.Any())
        {
            // 1. Read All Data from JSON file 'brand.json'
            // D:\backend\08 Asp.Net Core Web Apis\Session 02\New folder\Store\Infrastructure\Store.Persistance\Data\DataSeeding\brands.json
            var DeliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.Persistance\Data\DataSeeding\delivery.json");



            // 2. Convert the JsonString to List<ProductBrand>
            var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

            if (DeliveryMethods is not null && DeliveryMethods.Count > 0)
            {
                await _context.DeliveryMethods.AddRangeAsync(DeliveryMethods);
            }
        }


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

    public async Task InitializeIdentityAsync()
    {
        // Create DB
        // Update DB
        if ((await _identityContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await _identityContext.Database.MigrateAsync();
        }

        // Data Seed
        if (!_identityContext.Roles.Any())
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        }

        if (!_identityContext.Users.Any())
        {
            var superAdmin = new AppUser()
            {
                UserName = "SuperAdmin",
                DisplyName = "Supera Amin",
                Email = "SuperAdmin@gmail.com",
                PhoneNumber = "01234567890"
            };
            var admin = new AppUser()
            {
                UserName = "Admin",
                DisplyName = "Admin",
                Email = "Admin@gmail.com",
                PhoneNumber = "01234567891"
            };
            await _userManager.CreateAsync(superAdmin, "SuperAdmin123!");
            await _userManager.CreateAsync(admin, "Admin123!");

            await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            await _userManager.AddToRoleAsync(admin, "Admin");

        }



    }




}
