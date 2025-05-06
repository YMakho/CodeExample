using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.Api.Contracts.Bookings.Requests
{
    public sealed record GetBookingsByFilterRequest(
        long? UserId, 
        long? ResourceId, 
        BookingStatusDto? BookingStatus, 
        DateOnly? BookingDateOn,
        DateOnly? BookingDateOff, 
        DateTimeOffset? CreateBookingDate);
    
}
