using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Application.Contracts.Commands;
namespace BookingService.Booking.Application.Contracts.Bookings
{
    public interface IBookingsQueries
    {
        Task<BookingAggregate[]> GetByFilter(GetBookingByFilterCommand command);
        Task<BookingStatus> GetStatusById(long id);
    }
}
