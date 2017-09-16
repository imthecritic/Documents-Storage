using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DocumentStorage.Models.DB
{
    [Table("users")]
    public class User : IdentityUser<int>
    {
        [Key]
        public int UserID { get; set; }

        //[Required]
        //public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //[Required]
        //public string Email { get; set; }

    }
}
