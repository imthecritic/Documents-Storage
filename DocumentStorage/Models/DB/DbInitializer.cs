using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStorage.Models.DB
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{FirstName="John", LastName="Doe", Email= "johndoe@email.com", PasswordHash="abc"},
                new User{FirstName="Carson", LastName="Alexander", Email="calex@email.com", PasswordHash="abc"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
