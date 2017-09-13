using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DocumentStorage.Models.DB;

namespace DocumentStorage.Models
{
    public class CreateAccount
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

        public bool IsValid(string email, AppDbContext context)
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

    }
}
