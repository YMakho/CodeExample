using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.Api.Contracts.Bookings.Dtos
{
    public sealed record BookingDto
    {
        public long Id { get; init; }
        public BookingStatusDto Status { get; init; }
        public long UserId { get; init; }
        public long ResourceId { get; init; }
        public DateOnly BookingDateOn { get; init; }
        public DateOnly BookingDateOff { get; init; }
        public DateTimeOffset CreateBookingDate { get; init; } = DateTimeOffset.Now;
    }
}
