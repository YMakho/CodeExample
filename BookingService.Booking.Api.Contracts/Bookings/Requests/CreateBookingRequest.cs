
namespace BookingService.Booking.Api.Contracts.Bookings.Requests
{
    public sealed record CreateBookingRequest(
        long UserId, 
        long ResourceId, 
        DateOnly BookingDateOn,
        DateOnly BookingDateOff, 
        DateTimeOffset CreateBookingDate);
}
