using BookingService.Booking.Application.Contracts.Bookings;
using BookingService.Booking.Application.Contracts.Commands;
using BookingService.Booking.Application.Exceptions;
using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.Infrastructure
{
    public sealed class BookingsQueries : IBookingsQueries
    {
        private readonly BookingContext _context;
        public BookingsQueries(BookingContext context)
        {
            _context = context;
        }
        public Task<BookingAggregate[]> GetByFilter(GetBookingByFilterCommand command)
        {
            IQueryable<BookingAggregate> booking = _context.Bookings;

            if (command.UserId.HasValue && command.UserId > 0)
                booking = booking.Where(x => x.UserId.Equals(command.UserId));
            if (command.ResourceId.HasValue && command.ResourceId > 0)
                booking = booking.Where(x => x.ResourceId.Equals(command.ResourceId));
            if (command.Status.HasValue)
                booking = booking.Where(x => x.Status.Equals(command.Status));
            if (command.BookingDateOn.HasValue)
                booking = booking.Where(x => x.BookingDateOn.Equals(command.BookingDateOn));
            if (command.BookingDateOff.HasValue)
                booking = booking.Where(x => x.BookingDateOff.Equals(command.BookingDateOff));
            if (command.CreateBookingDate.HasValue)
                booking = booking.Where(x => x.CreateBookingDate.Equals(command.CreateBookingDate));
            var bookingArray = booking.ToArray();

            return Task.FromResult(bookingArray);
        }

        public Task<BookingStatus> GetStatusById(long id)
        {
            if (id <= 0)
            throw new ValidationException("Identificator should be greater than 0");

            var findBookingCheck = _context.Bookings.Find(id) ?? throw new ValidationException($"Booking with identificator {id} not found");
            BookingStatus result = _context.Bookings.Where(x => x.Id == id).Select(x => x.Status).FirstOrDefault();

            return Task.FromResult(result);
        }
    }
}
