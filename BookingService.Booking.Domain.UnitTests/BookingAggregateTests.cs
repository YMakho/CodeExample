using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Exceptions;

namespace BookingService.Booking.Domain.UnitTests
{
    public class BookingAggregateTests
    {
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetValidBookingAggregateInitializeData), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_Status_Confirmation_Test(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {
            //Arrange
            var booking = BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate);
            //Act
            booking.ConfirmBooking();
            //Assert
            Assert.Equal(BookingStatus.Confirmed, booking.Status);
        }
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetValidBookingAggregateInitializeData), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_Status_Cancelling_Test(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {
            //Arrange
            var booking = BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate);
            //Act
            booking.CancelBooking();
            //Assert
            Assert.Equal(BookingStatus.Cancelled, booking.Status);
        }
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetUnValidUserId), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_NotValidUserIdPassed_ShouldReturnDomainException(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate));
        }
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetUnValidResourceId), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_NotValidResourceIdPassed_ShouldReturnDomainException(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate));
        }
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetUnValidBookingDateOn), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_NotValidBookingDateOnPassed_ShouldReturnDomainException(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate));
        }
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetUnValidBookingDateOff), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_NotValidBookingDateOffPassed_ShouldReturnDomainException(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {
            // Act & Assert
            Assert.Throws<DomainException>(() => BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate));
        }
        [Theory]
        [MemberData(nameof(BookingAggregateTestData.GetUnValidCreatedBookingDate), MemberType = typeof(BookingAggregateTestData))]
        public void BookingAggregate_NotValidCreatedBookingDatePassed_ShouldReturnDomainException(long userId, long resourseId, DateOnly bookingDateOn, DateOnly bookingDateOff, DateTimeOffset createdBookingDate)
        {           
            // Act & Assert
            Assert.Throws<DomainException>(() => BookingAggregate.Initialize(userId, resourseId, bookingDateOn, bookingDateOff, createdBookingDate));
        }
    }
}
