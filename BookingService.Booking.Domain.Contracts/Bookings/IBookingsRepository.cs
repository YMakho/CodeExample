using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.Domain.Contracts.Bookings
{
    public interface IBookingsRepository
    {
        void Create(BookingAggregate aggregate);
        ValueTask<BookingAggregate?> GetById(long id, CancellationToken cancellationToken = default);
        void Update(BookingAggregate aggregate);
    }
}
