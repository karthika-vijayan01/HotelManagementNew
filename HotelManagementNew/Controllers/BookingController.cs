using HotelManagementNew.Models;
using HotelManagementNew.Repository;
using HotelManagementNew.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagementNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;

        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        #region 1 - Get all Bookings 
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _repository.GetTblBookings();
            if (bookings == null)
            {
                return NotFound("No Booking found");
            }
            return Ok(bookings);
        }
        #endregion

        #region 2 - Get an Bookings based on Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetAllBookingById(int id)
        {
            var book = await _repository.GetBookingsById(id);
            if (book == null)
            {
                return NotFound("No Bookings found");
            }
            return Ok(book);
        }
        #endregion

        #region   3  - Insert an BookingDetail 
        [HttpPost]
        public async Task<ActionResult<Booking>> InsertBookingsReturnRecord(Booking book)
        {
            if (ModelState.IsValid)
            {
                var newBooking = await _repository.PostBookingReturnRecord(book);
                if (newBooking != null)
                {
                    return Ok(newBooking);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region    4  - Update an Bookings with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateBookingsReturnRecord(int id, Booking book)
        {
            if (ModelState.IsValid)
            {
                var updateBooking = await _repository.PutTblBooking(id, book);
                if (updateBooking != null)
                {
                    return Ok(updateBooking);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  5  - Delete an Bookings
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            try
            {
                var result = _repository.DeleteBooking(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Booking could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region  6 - Get all GusestBooking Details
        [HttpGet("vm1")]
        public async Task<ActionResult<IEnumerable<GuestBookingHistory>>> GetAllVMGuestBookingHistory()
        {
            var gbh = await _repository.GetVMGuestBookingHistory();
            if (gbh == null)
            {
                return NotFound("No record found");
            }
            return Ok(gbh);
        }
        #endregion

        #region  7 - Get all CurrentBooking Details 
        [HttpGet("vm2")]
        public async Task<ActionResult<IEnumerable<CurrentBookings>>> GetAllVMCurrentBookings()
        {
            var gbh = await _repository.GetVMCurrentBookings();
            if (gbh == null)
            {
                return NotFound("No record found");
            }
            return Ok(gbh);
        }
        #endregion

        #region   8 - call stored procedure -- View Booking Details
        //[HttpGet("GetGuestReport/{guestId}")]
        //public async Task<IActionResult> GetGuestsReport(int guestId)
        //{
        //    try
        //    {
        //        var guestReport = await _repository.GetGuestReportById(guestId);

        //        if (guestReport == null || guestReport.Count == 0)
        //        {
        //            return NotFound(new { message = "No guest report found for the given ID." });
        //        }

        //        return Ok(guestReport);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (consider using a logging framework)
        //        return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        //    }
        //}

        #endregion
    }
}
