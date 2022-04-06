using DevelSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevelSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<EncuestaCab> EncuestaCabs { get; set; }
        public DbSet<EncuestaDet> EncuestaDets { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(nameof(User));
            modelBuilder.Entity<EncuestaCab>().ToTable(nameof(EncuestaCab));
            modelBuilder.Entity<EncuestaDet>().ToTable(nameof(EncuestaDet));
        }
    }
}
