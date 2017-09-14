using DocumentStorage.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentStorage.Models
{
    public class Dashboard
    {
        public int UserID { get; set; }

        public IEnumerable<File> Files { get; set; }

        public List<File> GatherFiles(int userId, AppDbContext context)
        {
            List<File> filesList = new List<File>();
            if (context != null)
            {
                var fileids = (from f in context.Files join uf in context.UsersFiles on f.FileID equals uf.FileID
                             where uf.UserID == userId && f.Active == true select f.FileID).ToList();

                if (fileids.Any())
                {
                    foreach (int id in fileids)
                    {
                        filesList.Add(context.Files.FirstOrDefault(t => t.FileID == id));
                    }
                }
            }
            return filesList;
        }
    }
}
