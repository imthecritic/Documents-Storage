using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DocumentStorage.Models.DB;
using System.Security.Cryptography;
using System.Text;

namespace DocumentStorage.Models
{
    public class AccountInfo
    {

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordReEnter { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public bool AccountNotFound(string email, AppDbContext context)
        {
            if (context != null)
            {

                var user = context.Users.Where(u => u.Email == email).ToList();
                if (user.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }

        }

        public string SecurePassword(string password)
        {
            MD5 md5Hash = MD5.Create();
            string hashedPassword = GetMd5Hash(md5Hash, password);
            return hashedPassword;
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
