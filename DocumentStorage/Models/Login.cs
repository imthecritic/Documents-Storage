using DocumentStorage.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStorage.Models
{
    public class Login
    {

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        public bool IsValid(string email, string password, AppDbContext context)
        {
            if (context != null)
            {

                var user = context.Users.Where(u => u.Email == email && u.PasswordHash == password).ToList();
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


    }
}
