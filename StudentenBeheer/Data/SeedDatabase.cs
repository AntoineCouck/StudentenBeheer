﻿using Microsoft.AspNetCore.Identity;
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
                    List<Language> languages = new List<Language>()
                    {
                        new Language(){Id = "de" , Name = "Deutch" , Cultures= "DE" , IsSystemLanguage = false},
                        new Language(){Id = "en" , Name = "English" , Cultures= "UK" , IsSystemLanguage = false},
                        new Language(){Id = "fr" , Name = "français" , Cultures= "BE;FR" , IsSystemLanguage = false},
                        new Language(){Id = "nl" , Name = "Nederlands" , Cultures= "BE;NL" , IsSystemLanguage = false}
                    };

                    context.SaveChanges();
                }


                ApplicationUser user = null;


                if (!context.Users.Any())
                {
                    user = new ApplicationUser
                    {
                        UserName = "Admin",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        Email = "System.administrator@studentenbeheer.be",
                        LanguageId = "BE-nl",
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


                // start initialisaties op basis van databank 

                List<string> supportedLanguages = new List<string>();
                Language.AllLanguages = context.Language.ToList();
                Language.LanguageDictionary = new Dictionary<string, Language>();
                Language.Systemlanguages = new List<Language>();



                supportedLanguages.Add("nl-BE");
                foreach (Language l in Language.AllLanguages)
                {
                    Language.LanguageDictionary[l.Id] = l;
                    supportedLanguages.Add(l.Id);
                    string[] even = l.Cultures.Split(";");
                    foreach (string e in even)
                    {
                        supportedLanguages.Add(l.Id + "-" + e);
                    }
                }
                Language.SupportedLanguages = supportedLanguages.ToArray();

            }



        }
    }

}

