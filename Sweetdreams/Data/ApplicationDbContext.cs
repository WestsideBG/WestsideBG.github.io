using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sweetdreams.Models;
using Sweetdreams.Models.Identity;
using System.Reflection.Emit;

namespace Sweetdreams.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasKey(u => u.Id);
            builder.Entity<ApplicationUser>().HasIndex(u => new { u.UserName, u.Email }).IsUnique();

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            base.OnModelCreating(builder);
        }


        public DbSet<Product> Products { get; set; }
    }
}