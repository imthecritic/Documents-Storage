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
            //modelBuilder.Entity<User>().HasKey(i => i.UserID);
            //modelBuilder.Entity<User>()
            //.Property(p => p.Id)
            //.HasColumnName("UserID");
            //getting rid of all of the unneeded things from identity framework
            //modelBuilder.Entity<User>().Ignore(c => c.AccessFailedCount)
            //                              .Ignore(c => c.ConcurrencyStamp)
            //                              .Ignore(c => c.EmailConfirmed)
            //                              .Ignore(c => c.LockoutEnabled)
            //                              .Ignore(c => c.LockoutEnd)
            //                              .Ignore(c => c.Id)
            //                              .Ignore(c => c.NormalizedEmail)
            //                              .Ignore(c => c.NormalizedUserName)
            //                              .Ignore(c => c.PhoneNumber)
            //                              .Ignore(c => c.PhoneNumberConfirmed)
            //                              .Ignore(c => c.TwoFactorEnabled)
            //                              ;
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<File>().ToTable("files");
            modelBuilder.Entity<UserFile>().ToTable("usersfiles");
            modelBuilder.Entity<UserRole>(i =>
            {
                i.HasKey(x => x.Id);
            });
            modelBuilder.Entity<IdentityUserRole<int>>(i =>
            {
                i.HasKey(x => new { x.RoleId, x.UserId });
            });
            modelBuilder.Entity<IdentityUserLogin<int>>(i =>
            {
                i.HasKey(x => new { x.ProviderKey, x.LoginProvider });
            });
            modelBuilder.Entity<IdentityRoleClaim<int>>(i =>
            {
                i.HasKey(x => x.Id);
            });
            modelBuilder.Entity<IdentityUserClaim<int>>(i =>
            {
                i.HasKey(x => x.Id);
            });
            modelBuilder.Entity<IdentityUserToken<int>>(i =>
            {
                i.HasKey(x => x.UserId);
            });
        }

    }
}
