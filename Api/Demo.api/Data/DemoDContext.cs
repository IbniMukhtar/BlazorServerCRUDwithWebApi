using Demo.api.Dmain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.api.Data
{
    public class DemoDContext : IdentityDbContext<IdentityUser>
    {
        public DemoDContext(DbContextOptions<DemoDContext> options) : base(options)
        {
        }

        // DbSet for your other entities (e.g., Product)
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configurations can be added here if needed
        }
    }
}
