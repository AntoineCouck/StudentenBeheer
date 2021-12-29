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



                ApplicationUser Beheerder = null;
                ApplicationUser Docent1 = null;
                ApplicationUser Student1 = null;


                if (!context.Users.Any())
                {
                    ApplicationUser dummy = new ApplicationUser { Id = "-", Firstname = "-", Lastname = "-", UserName = "-", Email = "?@?.?", LanguageId = "-" };
                    context.Users.Add(dummy);
                    context.SaveChanges();

                    Beheerder = new ApplicationUser
                    {
                        UserName = "Beheerder1",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        Email = "System.administrator@studentenbeheer.be",
                        LanguageId = "nl",
                        EmailConfirmed = true
                    };
                    Docent1 = new ApplicationUser
                    {
                        UserName = "Docent1",
                        Firstname = "Melvin",
                        Lastname = "Mars",
                        Email = "System.User@studentenbeheer.be",
                        LanguageId = "nl",
                        EmailConfirmed = true
                    };
                    Student1 = new ApplicationUser
                    {
                        UserName = "Student1",
                        Firstname = "Test",
                        Lastname = "Docent2",
                        Email = "System.User@studentenbeheer.be",
                        LanguageId = "nl",
                        EmailConfirmed = true
                    };



                    userManager.CreateAsync(Beheerder, "Abc!12345");
                    userManager.CreateAsync(Docent1, "Abc!12345");
                    userManager.CreateAsync(Student1, "Abc!12345");
                }



                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "Beheerder", Name = "Beheerder", NormalizedName = "beheerder" },
                            new IdentityRole { Id = "Docent", Name = "Docent", NormalizedName = "docent" },
                            new IdentityRole { Id = "Student", Name = "Student", NormalizedName = "student" }

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
                                   UserId = Student1.Id,
                                   Deleted = DateTime.MaxValue


                               },
                               new Student
                               {
                                   Name = "antoine2",
                                   Lastname = "Couck2",
                                   Birthday = DateTime.Now,
                                   GenderId = 'F',
                                   Deleted = DateTime.Now


                               },
                                 new Student
                                 {
                                     Name = "Melvin",
                                     Lastname = "Angeli",
                                     Birthday = DateTime.Now,
                                     GenderId = 'F',
                                     Deleted = DateTime.Now


                                 },
                                   new Student
                                   {
                                       Name = "Ine",
                                       Lastname = "Debast",
                                       Birthday = DateTime.Now,
                                       GenderId = 'F',
                                       Deleted = DateTime.Now


                                   }
                        );
                    context.SaveChanges();

                }

                if (!context.Docent.Any())
                {
                    context.Docent.AddRange(

                          new Docent
                          {
                              FirstName = "Docent1",
                              LastName = "Docent1",
                              Birthday = DateTime.Now,
                              GenderId = 'F',
                              UserId = Docent1.Id,
                              Email = "Docent1@docent.be",
                              DeletedAt = DateTime.MaxValue
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
                if (!context.Docenten_modules.Any())
                {
                    context.Docenten_modules.AddRange(

                        new Docenten_modules
                        {
                            ModuleId = 1,
                            DocentId = 1
                        
                        }

                        );
                }



                if (Beheerder != null && Docent1 != null && Student1 != null)
                {
                    context.UserRoles.AddRange(

                        new IdentityUserRole<string> { UserId = Beheerder.Id, RoleId = "Beheerder" },
                        new IdentityUserRole<string> { UserId = Docent1.Id, RoleId = "Docent" },
                        new IdentityUserRole<string> { UserId = Student1.Id, RoleId = "Student" }

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

