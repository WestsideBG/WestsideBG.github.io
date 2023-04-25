using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sweetdreams.Models.Identity;

namespace Sweetdreams.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasKey(u => u.Id);
            builder.Entity<ApplicationUser>().HasIndex(u => new { u.UserName, u.Email }).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}