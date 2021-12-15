using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Infrastructure.Internal;
using StudentenBeheer.Areas.Identity.Data;
using StudentenBeheer.Data;
using StudentenBeheer.Models;
using StudentenBeheer.Services.GroupSacePrep.Services;



var builder = WebApplication.CreateBuilder(args);



var connectionString = (builder.Configuration.GetConnectionString("ApplicationContextConnection"));
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>((IdentityOptions options) => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StudentenBeheer.Data.ApplicationContext>();


// localisation


builder.Services.AddMvc()
.AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization();



builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    option.SetDefaultCulture("nl-BE")
    .AddSupportedCultures(Language.SupportedLanguages)
    .AddSupportedUICultures(Language.SupportedLanguages);
});

// localisation

builder.Services.AddControllersWithViews();


// password settings
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // lockout settings

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // ApplicationUser settings

    options.User.RequireUniqueEmail = false;
});

builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
builder.Services.Configure<MailKitOptions>(options =>
{
    options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
    options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
    options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
    options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
    options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
    options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];

    // Set it to TRUE to enable ssl or tls, FALSE otherwise
    options.Security = false;  // true zet ssl or tls aan
});


// voor apparte een algemene controller te gebruiken 

builder.Services.AddHttpContextAccessor();

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
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    SeedDatabase.Initialize(services, userManager);
}

// voor de seeder
// voor localization

var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture("nl-BE")
.AddSupportedCultures(Language.SupportedLanguages)
.AddSupportedUICultures(Language.SupportedLanguages); app.UseRequestLocalization(localizationOptions);

// voor localization-
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// voor het gebruik van razor pages 

app.MapRazorPages();

app.Run();
