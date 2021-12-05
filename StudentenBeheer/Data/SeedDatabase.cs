using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Models;

namespace StudentenBeheer.Data
{

    public static class SeedDatabase
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentenBeheerContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StudentenBeheerContext>>()))
            {

                if (context.Gender.Any() || context.Student.Any())
                {
                    return;   // DB has been seeded
                }


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

                context.Module.AddRange(

                    new Module
                    {
                        Name = "Backend web",
                        Omschrijving = "iets met web te maken, denk ik",
                        Deleted = DateTime.MaxValue
                    },
                     new Module
                     {
                         Name = "Dynamic web",
                         Omschrijving = "iets met web te maken, denk ik maar met een rare taal",
                         Deleted = DateTime.Now
                     },
                      new Module
                      {
                          Name = "OS fundamentals",
                          Omschrijving = "iets echt raar, precies chinees",
                          Deleted = DateTime.MaxValue
                      }

                    );
                context.SaveChanges();

            }
        }
    }

}

