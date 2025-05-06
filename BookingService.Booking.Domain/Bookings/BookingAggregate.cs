using BookingService.Booking.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace BookingService.Booking.Domain.Bookings
{
    public sealed class BookingAggregate
    {
        [Key]
        public long Id { get; private set; }
        public BookingStatus Status { get; private set; }
        public long UserId { get; private set; }
        public long ResourceId { get; private set; }
        public Guid? CatalogRequestId { get; private set; }
        public DateOnly BookingDateOn { get; private set; }
        public DateOnly BookingDateOff { get; private set; }
        public DateTimeOffset CreateBookingDate { get; private set; }

        private BookingAggregate(
            long userId, 
            long resourceId, 
            DateOnly bookingDateOn, 
            DateOnly bookingDateOff, 
            DateTimeOffset createBookingDate)
        {
            Status = BookingStatus.AwaitConfirmation;
            UserId = userId;
            ResourceId = resourceId;
            BookingDateOn = bookingDateOn;
            BookingDateOff = bookingDateOff;
            CreateBookingDate = createBookingDate;
        }
        public void ConfirmBooking() => Status = BookingStatus.Confirmed;
        public void CancelBooking() => Status =  BookingStatus.Cancelled;
        public void SetCatalogRequestId(Guid catalogRequestId)
        {
            if (CatalogRequestId == null)
                CatalogRequestId = catalogRequestId;
        }
        public static BookingAggregate Initialize(
            long userId,
            long resourceId, 
            DateOnly bookingDateOn, 
            DateOnly bookingDateOff,
            DateTimeOffset createBookingDate)
        {
            if (userId <= 0)
                throw new DomainException($"Некорректный идентификатор пользователя {userId}");
            if (resourceId <= 0)
                throw new DomainException($"Некорректный идентификатор ресурса {resourceId}");
            if (bookingDateOn <= DateOnly.FromDateTime(createBookingDate.Date))
                throw new DomainException($"Дата начала бронирования должна быть больше текущей даты");
            if (bookingDateOff <= bookingDateOn)
                throw new DomainException($"Дата окончания бронирования должна быть больше начала бронирования");
            if (createBookingDate.Date != DateTimeOffset.UtcNow.Date)
                throw new DomainException($"Дата создания должна быть равна текущей дате");

            return new BookingAggregate(userId, resourceId, bookingDateOn, bookingDateOff, createBookingDate);
        }
    }
}

