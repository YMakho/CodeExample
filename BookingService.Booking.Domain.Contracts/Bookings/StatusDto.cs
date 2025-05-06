
namespace BookingService.Booking.Domain.Contracts.Bookings
{
    public enum StatusDto : byte
    {
        None = 0,
        AwaitConfirmation,
        Confirmed,
        Cancelled
    }
}
