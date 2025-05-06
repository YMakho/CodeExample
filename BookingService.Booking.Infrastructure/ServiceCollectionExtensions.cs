using BookingService.Booking.Domain.Contracts;
using BookingService.Booking.Domain.Contracts.Bookings;
using Microsoft.Extensions.DependencyInjection;
using BookingService.Booking.Application.Contracts.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            ArgumentNullException.ThrowIfNull(services);

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBookingsQueries, BookingsQueries>();
            services.AddScoped<IBookingsBackgroundQueries, BookingsBackgroundQueries>();
            services.AddDbContext<BookingContext>(
                (ctx, context) =>
                {
                    context.UseNpgsql(connectionString)
                        .UseLoggerFactory(ctx.GetRequiredService<ILoggerFactory>());
                }
            );

            return services;
        }
    }
}
