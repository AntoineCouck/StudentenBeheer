using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Areas.Identity.Data;

namespace StudentenBeheer.Data;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    public DbSet<StudentenBeheer.Models.Student> Student { get; set; }

    public DbSet<StudentenBeheer.Models.Gender> Gender { get; set; }

    public DbSet<StudentenBeheer.Models.Module> Module { get; set; }

    public DbSet<StudentenBeheer.Models.Inschrijvingen> Inschrijvingen { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
