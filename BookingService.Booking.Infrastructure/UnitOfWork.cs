using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.Domain.Contracts;

namespace BookingService.Booking.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;

        public IBookingsRepository BookingsRepository { get; }

        public UnitOfWork(BookingContext context, IBookingsRepository bookingsRepository)
        {
            _context = context;
            BookingsRepository = bookingsRepository;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
