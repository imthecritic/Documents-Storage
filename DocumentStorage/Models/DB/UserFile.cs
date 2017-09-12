using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentStorage.Models.DB
{
    [Table("usersfiles")]
    public class UserFile
    {
        public int RowID { get; set; }

        public int FileID { get; set; }

        public int UserID { get; set; }
    }
}
