using Microsoft.EntityFrameworkCore;
using MentalPeaceGuider.Models; 

namespace MentalPeaceGuider.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Link models to tables
        public DbSet<Users> Users { get; set; }
        public DbSet<AvailableSlot> AvailableSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRequest> BookingRequests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ImmediateCall> ImmediateCalls { get; set; }
        public DbSet<ChatbotInteraction> ChatbotInteractions { get; set; }
        public DbSet<MentalHealthProgress> MentalHealthProgress { get; set; }

        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<RescheduleRequest> RescheduleRequests { get; set; }
        public DbSet<CancelledBooking> CancelledBookings { get; set; }

        //  Override OnModelCreating for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly define primary key
            modelBuilder.Entity<AvailableSlot>()
                        .HasKey(a => a.SlotID);

            // Configure relationships
            modelBuilder.Entity<AvailableSlot>()
                        .HasOne(a => a.Counselor)
                        .WithMany(c => c.AvailableSlots)
                        .HasForeignKey(a => a.CounselorID);
           
        }
    }
}
