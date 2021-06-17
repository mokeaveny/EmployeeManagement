using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class IdentityContext : IdentityDbContext<Employee>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .HasMaxLength(250);
        }

    }
}