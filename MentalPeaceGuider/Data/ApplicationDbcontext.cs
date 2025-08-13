using Microsoft.EntityFrameworkCore;
//using MentalPeaceGuider.Models;

namespace MentalPeaceGuider.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Link your models to tables
        public DbSet<AvailableSlot> AvailableSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        // Optional: Override OnModelCreating for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: configure relationships or table names
            // modelBuilder.Entity<Booking>().ToTable("Bookings");
        }
    }
}
