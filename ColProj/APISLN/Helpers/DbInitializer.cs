using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Helpers
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            context.CollectiveUsers.Add(new CollectiveUser { CollectiveId = 1, UserId = 1 });
            context.SaveChanges();

            /* Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            byte[] passwordHash, passwordSalt;
            UserService.CreatePasswordHash("1234", out passwordHash, out passwordSalt);

            var users = new User[]
            {
                new User{Id=1,FirstName="FN1", LastName="LN1", Username="Un1", PasswordHash=passwordHash, PasswordSalt=passwordSalt}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
            */


        }
    }
}
