using Microsoft.AspNetCore.Mvc;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IUserShowTimeBookingService _userShowTimeBookingService;

        public BookingController(
            IUserShowTimeBookingService userShowTimeBookingService
        )
        {
            _userShowTimeBookingService = userShowTimeBookingService;
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<int>> CreateBookingAsync(
            BookingCreateDto bookingCreateDto
        )
        {
            return Ok(
                await _userShowTimeBookingService.CreateBookingAsync(
                    bookingCreateDto
                )
            );
        }
    }
}
