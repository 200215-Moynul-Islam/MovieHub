using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Services;

namespace MovieHub.API.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<BookingReadDto>> CreateBookingAsync(
            [FromBody] BookingCreateDto bookingCreateDto
        )
        {
            if (
                bookingCreateDto.UserId
                != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
            )
            {
                return Forbid();
            }

            return Created(
                String.Empty,
                await _userShowTimeBookingService.CreateBookingAsync(
                    bookingCreateDto
                )
            );
        }

        // GET: api/bookings?userId={userId}&offset={offset}&limit={limit}
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<BookingReadDto>>
        > GetAllBookingsWithSeatsByUserIdAsync(
            [FromQuery] Guid userId,
            [FromQuery] int offset = DefaultConstants.Offset,
            [FromQuery] int limit = DefaultConstants.Limit
        )
        {
            if (
                !User.IsInRole(DefaultConstants.Role.AdminRoleName)
                && !User.IsInRole(DefaultConstants.Role.MangerRoleName)
                && userId
                    != Guid.Parse(
                        User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                    )
            )
            {
                return Forbid();
            }

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
