using BookingService.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Infrastructure
{
    public sealed class BookingsBackgroundQueries : IBookingsBackgroundQueries
    {
        private readonly BookingContext _context;
        public BookingsBackgroundQueries(BookingContext context)
        {
            _context = context;
        }

        public async Task<BookingAggregate?> GetBookingByRequestId(Guid? requestId, CancellationToken cancellationToken = default)
        {
           var booking = await _context.Bookings.SingleAsync(x => x.CatalogRequestId == requestId);

            return booking;
        }
    }
}
