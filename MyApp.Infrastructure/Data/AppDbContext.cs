using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;


namespace MyApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
            .HasOne(e => e.AppUser) 
            .WithMany(u => u.Expenses) 
            .HasForeignKey(e => e.AppUserId) 
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
