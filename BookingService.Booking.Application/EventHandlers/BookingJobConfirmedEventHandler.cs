using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using SharedLibrary;

namespace BookingService.Booking.Application.EventHandlers
{
    public sealed class BookingJobConfirmedEventHandler : IHandleMessages<BookingJobConfirmed>
    {
        private readonly IBookingsBackgroundQueries _bookingsBackgroundQueries;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingJobConfirmedEventHandler> _logger;

        public BookingJobConfirmedEventHandler(
            IBookingsBackgroundQueries bookingsBackgroundQueries,
            IUnitOfWork unitOfWork,
            ILogger<BookingJobConfirmedEventHandler> logger)
        {
            _bookingsBackgroundQueries = bookingsBackgroundQueries;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(BookingJobConfirmed message)
        {
            var booking = await _bookingsBackgroundQueries.GetBookingByRequestId(message.Id);

            if (booking == null)
            {
                _logger.LogWarning($"Booking with identificator: {message.Id} not found");

                return;
            }

            _logger.LogInformation($"Reseive a booking: {message.Id} confirmation");
            booking.ConfirmBooking();
            _unitOfWork.BookingsRepository.Update(booking);

            await _unitOfWork.CommitAsync();
        }
    }
}
