using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DocumentStorage.Models.DB
{
    public class AppDbContext : IdentityDbContext<User, UserRole, int>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public new DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<UserFile> UsersFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(i => i.UserID);

            modelBuilder.Entity<User>().Ignore(c => c.AccessFailedCount)
                                          .Ignore(c => c.ConcurrencyStamp)
                                          .Ignore(c => c.EmailConfirmed)
                                          .Ignore(c => c.Id)
                                          .Ignore(c => c.LockoutEnabled)
                                          .Ignore(c => c.LockoutEnd)
                                          .Ignore(c => c.NormalizedEmail)
                                          .Ignore(c => c.NormalizedUserName)
                                          .Ignore(c => c.PhoneNumber)
                                          .Ignore(c => c.PhoneNumberConfirmed)
                                          .Ignore(c => c.SecurityStamp)
                                          .Ignore(c => c.TwoFactorEnabled)
                                          .Ignore(c => c.UserName)
                                          ;
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<File>().ToTable("files");
            modelBuilder.Entity<UserFile>().ToTable("usersfiles");
        }

    }
}
