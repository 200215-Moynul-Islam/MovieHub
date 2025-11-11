using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IUserBookingService _userBookingService;
        private readonly IUserShowTimeBookingService _userShowTimeBookingService;

        public BookingController(
            IUserBookingService userBookingService,
            IUserShowTimeBookingService userShowTimeBookingService
        )
        {
            _userBookingService = userBookingService;
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

        // GET: api/bookings?userId=123
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<BookingReadDto>>
        > GetAllBookingsWithSeatsByUserIdAsync(
            [FromQuery] Guid userId,
            [FromQuery] int offset = DefaultConstants.Offset,
            [FromQuery] int limit = DefaultConstants.Limit
        )
        {
            return Ok(
                await _userBookingService.GetAllBookingsWithSeatsByUserIdAsync(
                    userId,
                    offset,
                    limit
                )
            );
        }
    }
}
