using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace DocumentStorage.Models.DB
{
    [Table("files")]
    public class File
    {
        
        public int FileID { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public int Downloads { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
