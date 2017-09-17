using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentStorage.Models.DB
{
    [Table("usersfiles")]
    public class UserFile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RowID { get; set; }

        [Required]
        public int FileID { get; set; }

        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User user { get; set; }

        [ForeignKey("FileID")]
        public File file { get; set; }
    }
}
