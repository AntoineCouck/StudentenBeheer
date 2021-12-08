using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Areas.Identity.Data;
using StudentenBeheer.Models;

namespace StudentenBeheer.Data
{

    public static class SeedDatabase
    {

        public static void Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                ApplicationUser user = null;
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    user = new ApplicationUser
                    {
                        UserName = "Admin",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user, "Abc!98765");
                }

                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "User", Name = "User", NormalizedName = "user" },
                            new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "admin" }

                            );

                    context.SaveChanges();
                }


                if (!context.Gender.Any() || !(context.Student.Any()))
                {
                    // DB has been seeded
                    context.Gender.AddRange(

                       new Gender
                       {

                           ID = 'M',
                           Name = "Male"


                       },

                       new Gender
                       {

                           ID = 'F',
                           Name = "Female"


                       },

                       new Gender
                       {
                           ID = 'X',
                           Name = "Not set"
                       }

                   );
                    context.SaveChanges();

                    context.Student.AddRange(

                               new Student
                               {
                                   Name = "antoine",
                                   Lastname = "Couck",
                                   Birthday = DateTime.Now,
                                   GenderId = 'M',
                                   Deleted = DateTime.MaxValue


                               },
                               new Student
                               {
                                   Name = "antoine2",
                                   Lastname = "Couck2",
                                   Birthday = DateTime.Now,
                                   GenderId = 'F',
                                   Deleted = DateTime.Now


                               }
                        );
                    context.SaveChanges();

                }

                if (!context.Module.Any())
                {
                    context.Module.AddRange(

                    new Module
                    {
                        Name = "Backend web",
                        Omschrijving = "Iets met web te maken, denk ik",
                        Deleted = DateTime.MaxValue
                    },
                     new Module
                     {
                         Name = "Dynamic web",
                         Omschrijving = "Iets met web te maken, denk ik maar met een rare taal",
                         Deleted = DateTime.Now
                     },
                      new Module
                      {
                          Name = "OS fundamentals",
                          Omschrijving = "Iets echt raar, precies chinees",
                          Deleted = DateTime.MaxValue
                      }

                    );

                    context.SaveChanges();

                }


                if (user != null)
                {
                    context.UserRoles.AddRange(

                        new IdentityUserRole<string> { UserId = user.Id, RoleId = "Admin" }
                        //new IdentityUserRole<string> { UserId = user.Id, RoleId = "User" }

                        );

                    context.SaveChanges();
                }


            }
          


        }
    }

}

