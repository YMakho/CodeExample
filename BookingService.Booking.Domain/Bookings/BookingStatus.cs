namespace BookingService.Booking.Domain.Bookings
{
    public enum BookingStatus : byte
    {
        AwaitConfirmation = 1,
        Confirmed,
        Cancelled
    }
}
