using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Infrastructure
{
    public sealed class BookingContext : DbContext
    {
        public DbSet<BookingAggregate> Bookings { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingAggregateConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
