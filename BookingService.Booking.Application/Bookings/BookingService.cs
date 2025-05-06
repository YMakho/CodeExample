using BookingService.Booking.Application.Contracts.Bookings;
using BookingService.Booking.Application.Contracts.Commands;
using BookingService.Booking.Application.EventHandlers;
using BookingService.Booking.Application.Exceptions;
using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts;
using Rebus.Bus;
using Rebus.Messages;
using SharedLibrary;
using System.Reflection;
using System.Text;

namespace BookingService.Booking.Application.Bookings
{
    public sealed class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;
        public BookingService(IUnitOfWork unitOfWork, IBus bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
        }
        public async Task Cancel(long id)
        {
            var booking = await _unitOfWork.BookingsRepository.GetById(id) ?? throw new ValidationException($"Can't find booking with identificator: {id}");
            booking.CancelBooking();

            if (booking.CatalogRequestId == null)
            {
                _unitOfWork.BookingsRepository.Update(booking);
                await _unitOfWork.CommitAsync();
            }

            await _bus.Publish(new CancelBookingJobByRequestIdRequest(booking.CatalogRequestId));
        }

        public async Task<long> Create(CreateBookingCommand command)
        {
            var createdBooking = BookingAggregate.Initialize(command.UserId, command.ResourceId, command.BookingDateOn, command.BookingDateOff, command.CreateBookingDate);
            createdBooking.SetCatalogRequestId(Guid.NewGuid());

            _unitOfWork.BookingsRepository.Create(createdBooking);
            await _unitOfWork.CommitAsync(default);
            await _bus.Publish(new CreateBookingJobRequest(createdBooking.CatalogRequestId));

            return createdBooking.Id;
        }
        public async ValueTask<BookingAggregate> GetById(long id)
        {
            var booking = await _unitOfWork.BookingsRepository.GetById(id);

            return booking ?? throw new ValidationException($"Can't find booking with identificator: {id}");
        }
    }
}
