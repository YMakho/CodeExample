using BookingService.Booking.Api.Contracts.Bookings;
using BookingService.Booking.Api.Contracts.Bookings.Dtos;
using BookingService.Booking.Api.Contracts.Bookings.Requests;
using BookingService.Booking.Application.Contracts.Bookings;
using BookingService.Booking.Application.Contracts.Commands;
using BookingService.Booking.Application.Contracts.Mapping;
using BookingService.Booking.Domain.Contracts.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Booking.Api.Controllers
{
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingsQueries _bookingsQueries;
        public BookingsController(
            IBookingService bookingService, 
            IBookingsQueries bookingsQueries)
        {
            _bookingService = bookingService;
            _bookingsQueries = bookingsQueries;
        }

        [HttpGet(WebRoutes.GetById)]
        public async Task<BookingDto> GetById(long id)
        {
            var booking = await _bookingService.GetById(id);

            return BookingMapping.ToBookingDto(booking);
        }

        [HttpGet(WebRoutes.GetStatusById)]
        public async Task<BookingStatusDto> GetStatusById(long id)
        {
            var bookingStatus = await _bookingsQueries.GetStatusById(id);

            return BookingStatusMapping.ToBookingStatusDto(bookingStatus);
        }

        [HttpGet(WebRoutes.GetByFilter)]
        public async Task<BookingDto[]> GetByFilter([FromQuery] GetBookingsByFilterRequest request)
        {
            var command = GetBookingByFilterCommand.ToCommand(request);
            var responce = await _bookingsQueries.GetByFilter(command);

            return BookingMapping.ToBookingDto(responce);
        }

        [HttpPost(WebRoutes.Create)]
        public async Task<long> Create([FromBody] CreateBookingRequest request)
        {
            var command = CreateBookingCommand.ToCommand(request);
            var responce = await _bookingService.Create(command);

            return responce;
        }

        [HttpDelete(WebRoutes.Cancel)]
        public async Task Cancel(long id)
        {
            await _bookingService.Cancel(id);
        }
    }
}