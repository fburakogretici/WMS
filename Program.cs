using Microsoft.EntityFrameworkCore;
using IndexWMSProject.Data;
using IndexWMSProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WmsDbContext>(options =>
    options.UseInMemoryDatabase("WmsTestDb"));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WmsDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Products.Any())
    {
        var p1 = new Product { Barcode = "8690001", Name = "Laptop Dell XPS 15", StockQuantity = 50, Location = "A1-01" };
        var p2 = new Product { Barcode = "8690002", Name = "Logitech MX Master 3", StockQuantity = 120, Location = "B2-10" };
        var p3 = new Product { Barcode = "8690003", Name = "Dell 27 Monitor", StockQuantity = 30, Location = "C3-05" };
        
        dbContext.Products.AddRange(p1, p2, p3);
        dbContext.SaveChanges();

        var order = new B2BOrder
        {
            OrderNumber = "ORD-2023-001",
            OrderDate = DateTime.Now,
            CustomerName = "Teknoloji A.Ş.",
            Status = "Pending",
            Items = new List<B2BOrderItem>
            {
                new B2BOrderItem { ProductId = p1.Id, Quantity = 2, IsPicked = false },
                new B2BOrderItem { ProductId = p2.Id, Quantity = 5, IsPicked = false }
            }
        };
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
