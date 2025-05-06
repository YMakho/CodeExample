using BookingService.Booking.Application.Contracts.Commands;
using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.Application.Contracts.Bookings
{
    public interface IBookingService
    {
        Task<long> Create(CreateBookingCommand command);
        ValueTask<BookingAggregate> GetById(long id);
        Task Cancel(long id);
    }
}
