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
                context.Database.EnsureCreated();

                if (!context.Language.Any())
                {
                    context.Language.AddRange(
                        new Language() { Id = "-", Name = "-", Cultures = "-", IsSystemLanguage = false },
                        new Language() { Id = "de", Name = "Deutsch", Cultures = "DE", IsSystemLanguage = false },
                        new Language() { Id = "en", Name = "English", Cultures = "UK;US", IsSystemLanguage = true },
                        new Language() { Id = "es", Name = "Español", Cultures = "ES", IsSystemLanguage = false },
                        new Language() { Id = "fr", Name = "français", Cultures = "BE;FR", IsSystemLanguage = true },
                        new Language() { Id = "nl", Name = "Nederlands", Cultures = "BE;NL", IsSystemLanguage = true }
                    );
                    context.SaveChanges();
                }



                ApplicationUser user = null;
                ApplicationUser user2 = null;


                if (!context.Users.Any())
                {
                    ApplicationUser dummy = new ApplicationUser { Id = "-", Firstname = "-", Lastname = "-", UserName = "-", Email = "?@?.?", LanguageId = "-" };
                    context.Users.Add(dummy);
                    context.SaveChanges();

                    user = new ApplicationUser
                    {
                        UserName = "SuperAdmin",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        Email = "System.administrator@studentenbeheer.be",
                        LanguageId = "nl",
                        EmailConfirmed = true
                    };
                    user2 = new ApplicationUser
                    {
                        UserName = "User",
                        Firstname = "Melvin",
                        Lastname = "Mars",
                        Email = "System.User@studentenbeheer.be",
                        LanguageId = "nl",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user, "Abc!98765");
                    userManager.CreateAsync(user2, "Abc!12345");
                }

                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "SuperBeheerder", Name = "SuperBeheerder", NormalizedName = "admin" },
                            new IdentityRole { Id = "Beheerder", Name = "Beheerder", NormalizedName = "beheerder" },
                            new IdentityRole { Id = "Docent", Name = "Docent", NormalizedName = "docent" },
                            new IdentityRole { Id = "Student" , Name = "Student" , NormalizedName = "student"}

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

                        new IdentityUserRole<string> { UserId = user.Id, RoleId = "SuperBeheerder" }
                        //new IdentityUserRole<string> { UserId = user.Id, RoleId = "User" }

                        );

                    context.SaveChanges();
                }


              
            // Start initialisatie talen op basis van databank

            List<string> supportedLanguages = new List<string>();
            Language.AllLanguages = context.Language.ToList();
            Language.LanguageDictionary = new Dictionary<string, Language>();
            Language.SystemLanguages = new List<Language>();

            supportedLanguages.Add("nl-BE");
            foreach (Language l in Language.AllLanguages)
                {  
                    
                    // key not found = ligne en dessous mettre au dessus du if 
                    Language.LanguageDictionary[l.Id] = l;


                    if (l.Id != "-")
                {
                     
                    if (l.IsSystemLanguage)
                        Language.SystemLanguages.Add(l);
                    supportedLanguages.Add(l.Id);
                    string[] even = l.Cultures.Split(";");
                    foreach (string e in even)
                    {
                        supportedLanguages.Add(l.Id + "-" + e);
                    }
                }
            }
            Language.SupportedLanguages = supportedLanguages.ToArray();

            }

        }
    }

}

