using BookingService.Booking.Application.Bookings;
using BookingService.Booking.Domain.Exceptions;
using BookingService.Booking.Application.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.EntityFrameworkCore;
using BookingService.Booking.Infrastructure;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Microsoft.Extensions.Options;
using Rebus.Serialization.Json;
using SharedLibrary;

namespace BookingService.Booking.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAppServices();
            services.AddInfrastructure(_configuration.GetConnectionString("DefaultConnection"));
            services.Configure<RebusRabbitMqOptions>(_configuration.GetSection("RebusRabbitMqOptions"));
            services.AddRebus((builder, ctx) => builder

                    .Transport(t =>
                               t.UseRabbitMq(ctx.GetRequiredService<IOptions<RebusRabbitMqOptions>>().Value.ConnectionString, "booking-service-booking-queue")
                    .DefaultQueueOptions(queue => queue.SetDurable(true))
                    .ExchangeNames("booking-service-booking-direct", "booking-service-topics"))
                    .Serialization(s => s.UseSystemTextJson())
                    .Logging(l => l.Serilog())
                    .Routing(r => r.TypeBased()
                    .Map<CreateBookingJobRequest>("booking-service-booking-queue")
                    .Map<CancelBookingJobByRequestIdRequest>("booking-service-booking-queue")),
                    onCreated: async bus =>
                    {
                        await bus.Subscribe<BookingJobConfirmed>();
                        await bus.Subscribe<BookingJobDenied>();
                    });
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (context, ex) =>
                {
                    var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
                    return environment.IsDevelopment();
                };
                options.Map<ValidationException>(exception => new ProblemDetails
                {
                    Title = exception.Message,
                    Detail = exception.StackTrace,
                    Status = StatusCodes.Status400BadRequest
                });
                options.Map<DomainException>(exception => new ProblemDetails
                {
                    Title = exception.Message,
                    Detail = exception.StackTrace,
                    Status = StatusCodes.Status402PaymentRequired
                });
                options.OnBeforeWriteDetails = (ctx, problem) =>
                {
                    Log.Error("You have an exception");
                    Log.Error(problem.Title);
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseProblemDetails();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
