using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DocumentStorage.Models.DB
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            MD5 md5Hash = MD5.Create();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{FirstName="John", LastName="Doe", Email= "johndoe@email.com", PasswordHash=GetMd5Hash(md5Hash, "abc")},
                new User{FirstName="Carson", LastName="Alexander", Email="calex@email.com",PasswordHash= GetMd5Hash(md5Hash, "abcd")},
                new User{FirstName="Jasmine", LastName="Farley", Email="jfarley@email.com",PasswordHash= GetMd5Hash(md5Hash, "xyz")}

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

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

    }
}
