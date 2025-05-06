using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.Application.Contracts.Mapping
{
    public static class BookingStatusMapping
    {
        public static BookingStatus? ToDomain(BookingStatusDto dto)
        {
            if (dto == null) return null;
            if(Enum.TryParse(dto.Status, out BookingStatus bookingStatus) && Enum.IsDefined(typeof(BookingStatus), bookingStatus))
                return bookingStatus;
            return null;

        }
        public static BookingStatusDto ToBookingStatusDto(BookingStatus bookingStatus)
        {
            return new BookingStatusDto { Status = bookingStatus.ToString() };
        }
    }
}
