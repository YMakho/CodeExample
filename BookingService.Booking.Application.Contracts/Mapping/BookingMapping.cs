using BookingService.Booking.Api.Contracts.Bookings.Dtos;
using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.Application.Contracts.Mapping
{
    public static class BookingMapping
    {
        public static BookingDto ToBookingDto(BookingAggregate bookingData)
        {
            var bookingDto = new BookingDto
            {
                Id = bookingData.Id,
                Status = BookingStatusMapping.ToBookingStatusDto(bookingData.Status),
                UserId = bookingData.UserId,
                ResourceId = bookingData.ResourceId,
                BookingDateOn = bookingData.BookingDateOn,
                BookingDateOff = bookingData.BookingDateOff,
                CreateBookingDate = bookingData.CreateBookingDate
            };

            return bookingDto;
        }
        public static BookingDto[] ToBookingDto(BookingAggregate[] bookingDataArray)
        {
            var bookingDtoArray = bookingDataArray.Select(x => new BookingDto
            {
                Id = x.Id,
                Status = BookingStatusMapping.ToBookingStatusDto(x.Status),
                UserId = x.UserId,
                ResourceId = x.ResourceId,
                BookingDateOn = x.BookingDateOn,
                BookingDateOff = x.BookingDateOff,
                CreateBookingDate = x.CreateBookingDate
            })
                .ToArray();

            return bookingDtoArray;
        }
        public static BookingAggregate ToBookingData(BookingDto bookingDto)
        {
            var bookingData = BookingAggregate.Initialize(bookingDto.UserId, bookingDto.ResourceId, bookingDto.BookingDateOn, bookingDto.BookingDateOff, bookingDto.CreateBookingDate);

            return bookingData;
        }
        
    }
}
