using BookingService.Booking.Application.Contracts.Bookings;
using BookingService.Booking.Application.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
namespace BookingService.Booking.Application.Bookings
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingService>();
            services.AddRebusHandler<BookingJobConfirmedEventHandler>();
            services.AddRebusHandler<BookingJobDeniedEventHandler>();

            return services;
        }
    }
}
