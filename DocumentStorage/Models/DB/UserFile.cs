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
        public int RowID { get; set; }

        public int FileID { get; set; }

        public int UserID { get; set; }
    }
}
