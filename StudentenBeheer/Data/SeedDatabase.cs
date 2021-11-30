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
                               GenderId = 'M'


                           },
                           new Student
                           {
                               Name = "antoine2",
                               Lastname = "Couck2",
                               Birthday = DateTime.Now,
                               GenderId = 'F'


                           }
                    );
                context.SaveChanges();


            }
        }
    }

}

