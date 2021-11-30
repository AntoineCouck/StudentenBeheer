using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentenBeheerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentenBeheerContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

// voor de seeder 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedDatabase.Initialize(services);
}

// voor de seeder

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
