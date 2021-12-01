using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Data;
using Microsoft.AspNetCore.Identity;
using StudentenBeheer.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

var connectionString = (builder.Configuration.GetConnectionString("StudentenBeheerContext"));

builder.Services.AddDbContext<StudentenBeheerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentenBeheerContext")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
