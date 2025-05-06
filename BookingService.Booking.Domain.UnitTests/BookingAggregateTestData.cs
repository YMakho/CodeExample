
namespace BookingService.Booking.Domain.UnitTests
{
    public class BookingAggregateTestData
    {
        private static DateOnly Today = DateOnly.FromDateTime(DateTime.UtcNow);
        public static IEnumerable<object[]> GetUnValidUserId()
        {
            yield return new object[] { 0, 1, Today.AddDays(1), Today.AddDays(10), DateTimeOffset.UtcNow.Date };
            yield return new object[] { 0, 4, Today.AddDays(1), Today.AddDays(10), DateTimeOffset.UtcNow.Date };
        }
        public static IEnumerable<object[]> GetUnValidResourceId()
        {
            yield return new object[] { 1, 0, Today.AddDays(1), Today.AddDays(10), DateTimeOffset.UtcNow.Date };
            yield return new object[] { 1, -1, Today.AddDays(1), Today.AddDays(10), DateTimeOffset.UtcNow.Date };
        }
        public static IEnumerable<object[]> GetUnValidBookingDateOn()
        {
            yield return new object[] { 1, 8, Today, Today.AddDays(10), DateTimeOffset.UtcNow.Date };
            yield return new object[] { 1, 6, Today.AddDays(-1), Today.AddDays(10), DateTimeOffset.UtcNow.Date };
        }
        public static IEnumerable<object[]> GetUnValidBookingDateOff()
        {
            yield return new object[] { 1, 2, Today.AddDays(1), Today.AddDays(-1), DateTimeOffset.UtcNow.Date };
            yield return new object[] { 1, 1, Today.AddDays(1), Today, DateTimeOffset.UtcNow.Date };
        }
        public static IEnumerable<object[]> GetUnValidCreatedBookingDate()
        {
            yield return new object[] { 1, 1, Today.AddDays(3), Today.AddDays(10), DateTimeOffset.Parse(Today.AddDays(-1).ToString()) };
            yield return new object[] { 1, 9, Today.AddDays(3), Today.AddDays(10), DateTimeOffset.Parse(Today.AddDays(1).ToString()) };
        }
        public static IEnumerable<object[]> GetValidBookingAggregateInitializeData()
        {
            yield return new object[] { 1, 1, Today.AddDays(3), Today.AddDays(10), DateTimeOffset.UtcNow.Date };
            yield return new object[] { 4, 9, Today.AddDays(1), Today.AddDays(3), DateTimeOffset.UtcNow.Date };
        }
    }
}
