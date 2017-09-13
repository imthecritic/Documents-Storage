using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace DocumentStorage.Models.DB
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<UserFile> UsersFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<File>().ToTable("files");
            modelBuilder.Entity<UserFile>().ToTable("usersfiles");
        }

    }
}
