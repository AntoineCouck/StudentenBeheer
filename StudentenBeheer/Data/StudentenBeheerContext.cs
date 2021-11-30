using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Models;

namespace StudentenBeheer.Data
{
    public class StudentenBeheerContext : DbContext
    {
        public StudentenBeheerContext(DbContextOptions<StudentenBeheerContext> options)
            : base(options)
        {
        }

        public DbSet<StudentenBeheer.Models.Student> Student { get; set; }

        public DbSet<StudentenBeheer.Models.Gender> Gender { get; set; }

        public DbSet<StudentenBeheer.Models.Module> Module { get; set; }
    }
}
