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

            var files = new File[]
            {
                new File{FileName="test", FilePath="fakepath", Downloads = 0, Active= true, Created = new DateTime(2017,01,01)},
                new File{FileName="test2", FilePath="fakepath2", Downloads = 0, Active= true, Created = new DateTime(2017,08,01)},
                new File{FileName="test2", FilePath="fakepath2", Downloads = 0, Active= true, Created = new DateTime(2016,08,01)}

            };

            foreach (File f in files)
            {
                context.Files.Add(f);
            }

            context.SaveChanges();

            var usersfiles = new UserFile[]
            {
                new UserFile{UserID=1, FileID=2},
                new UserFile{UserID=2, FileID=3},
                new UserFile{UserID=1, FileID=1},
            };

            foreach (UserFile uf in usersfiles)
            {
                context.UsersFiles.Add(uf);
            }

            context.SaveChanges();

        }
    }
}
