using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Areas.Identity.Data;
using StudentenBeheer.Data;
var builder = WebApplication.CreateBuilder(args);

var connectionString = (builder.Configuration.GetConnectionString("StudentenBeheerContext"));

builder.Services.AddDbContext<StudentenBeheerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentenBeheerContext")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>(); builder.Services.AddDbContext<IdentityContext>(options =>
     options.UseSqlServer(connectionString));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // lockout settings

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings

    options.User.RequireUniqueEmail = false;
});
var app = builder.Build();

// password settings



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
