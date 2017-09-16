using DocumentStorage.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DocumentStorage.Models
{
    public class Login
    {

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        public int UserID { get; set; }

        public bool IsValid(string email, string password, AppDbContext context)
        {
            if (context != null)
            {
                MD5 md5Hash = MD5.Create();
                string hashed = GetMd5Hash(md5Hash, password);
    
                var user = context.Users.Where(u => u.Email == email && u.PasswordHash == hashed).ToList();
                if (user.Any())
                {
      
                        return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

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
