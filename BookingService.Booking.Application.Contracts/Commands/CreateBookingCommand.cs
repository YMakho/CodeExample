using BookingService.Booking.Api.Contracts.Bookings.Requests;

namespace BookingService.Booking.Application.Contracts.Commands
{
    public sealed class CreateBookingCommand 
    {
        public long UserId { get; private set; }
        public long ResourceId { get; private set; }
        public DateOnly BookingDateOn { get; private set; }
        public DateOnly BookingDateOff { get; private set; }
        public DateTimeOffset CreateBookingDate { get; private set; }

        public CreateBookingCommand(
            long userId,
            long resourceId, 
            DateOnly bookingDateOn,
            DateOnly bookingDateOff, 
            DateTimeOffset createBookingDate)
        {
            UserId = userId;
            ResourceId = resourceId;
            BookingDateOn = bookingDateOn;
            BookingDateOff = bookingDateOff;
            CreateBookingDate = createBookingDate;
        }
        public static CreateBookingCommand ToCommand(CreateBookingRequest request)
        {
            return new CreateBookingCommand(request.UserId, request.ResourceId, request.BookingDateOn, request.BookingDateOff, request.CreateBookingDate);
        }
    }
}
