using BookingService.Booking.Domain.Contracts.Bookings;
namespace BookingService.Booking.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IBookingsRepository BookingsRepository { get; }

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
