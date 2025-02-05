using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Context
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly); //Apply configurations created in the configuration file
            //modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(ur => ur.User)
            //    .WithMany(u => u.UserRoles)
            //    .HasForeignKey(u => u.UserId);

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(u => u.Role)
            //    .WithMany(u => u.UserRoles)
            //    .HasForeignKey(u => u.RoleId);

            //modelBuilder.Entity<Blog>()
            //    .HasOne(b => b.User)
            //    .WithMany(b => b.Blogs)
            //    .HasForeignKey(b => b.UserId);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(u => u.User)
            //    .WithMany(c => c.Comments)
            //    .HasForeignKey(u => u.UserId);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(b => b.Blog)
            //    .WithMany(b => b.Comments)
            //    .HasForeignKey(bi => bi.BlogId);

        }
    }
}
