using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using SharedLibrary;

namespace BookingService.Booking.Application.EventHandlers
{
    public sealed class BookingJobDeniedEventHandler : IHandleMessages<BookingJobDenied>
    {
        private readonly IBookingsBackgroundQueries _bookingsBackgroundQueries;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookingJobDeniedEventHandler> _logger;
        
        public BookingJobDeniedEventHandler(
            IBookingsBackgroundQueries bookingsBackgroundQueries,
            IUnitOfWork unitOfWork, 
            ILogger<BookingJobDeniedEventHandler> logger)
        {
            _bookingsBackgroundQueries = bookingsBackgroundQueries;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task Handle(BookingJobDenied message)
        {
            var booking = await _bookingsBackgroundQueries.GetBookingByRequestId(message.Id);

            if (booking == null)
            {
                _logger.LogWarning($"Booking with identificator: {message.Id} not found");

                return;
            }
            booking.CancelBooking();
            _logger.LogInformation($"Reseive booking: {message.Id} denied");
            _unitOfWork.BookingsRepository.Update(booking);

            await _unitOfWork.CommitAsync();
        }
    }
}
