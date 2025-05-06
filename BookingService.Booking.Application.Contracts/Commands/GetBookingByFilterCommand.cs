using BookingService.Booking.Api.Contracts.Bookings.Requests;
using BookingService.Booking.Application.Contracts.Mapping;
using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.Application.Contracts.Commands
{
    public sealed class GetBookingByFilterCommand
    {
        public long? UserId { get; set; }
        public long? ResourceId { get; set; }
        public BookingStatus? Status { get; set; }
        public DateOnly? BookingDateOn { get; set; }
        public DateOnly? BookingDateOff { get; set; }
        public DateTimeOffset? CreateBookingDate { get; set; }
        public GetBookingByFilterCommand(
            long? userId, 
            long? resourceId, 
            BookingStatusDto? bookingStatus, 
            DateOnly? bookingDateOn, 
            DateOnly? bookingDateOff,
            DateTimeOffset? createBookingDate)
        {
            UserId = userId;
            ResourceId = resourceId;
            Status = BookingStatusMapping.ToDomain(bookingStatus);
            BookingDateOn = bookingDateOn;
            BookingDateOff = bookingDateOff;
            CreateBookingDate = createBookingDate;
        }
        public static GetBookingByFilterCommand ToCommand(GetBookingsByFilterRequest request)
        {
            return new GetBookingByFilterCommand(
                request.UserId,
                request.ResourceId,
                request.BookingStatus,
                request.BookingDateOn,
                request.BookingDateOff, 
                request.CreateBookingDate);
        }
    }
}
