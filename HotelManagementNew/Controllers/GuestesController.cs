using HotelManagementNew.Models;
using HotelManagementNew.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestesController : Controller
    {
        private readonly IGuestRepository _repository;
        //DI -- constructor injection
        public GuestesController(IGuestRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guest>>> GetAllBooks()
        {
            var employees = await _repository.GetAllGuest();
            if (employees == null)
            {
                return NotFound("No Guest found ");
            }
            return Ok(employees);
        }

        #region Search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetBookById(int id)
        {
            var book = await _repository.GetGuestById(id);
            if (book == null)
            {
                return NotFound("No Guest found ");
            }
            return Ok(book);
        }
        #endregion
        #region 4 insert a Guest record
        [HttpPost]
        public async Task<ActionResult<Guest>> postBookaReturnRecord(Guest bk)
        {
            if (ModelState.IsValid)
            {
                var newbook = await _repository.postGuestReturnRecord(bk);
                if (newbook != null)
                {
                    return Ok(newbook);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region 5 insert an postGuestReturnId return Guest by id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> postGuestaReturnId(Guest bk)
        {
            if (ModelState.IsValid)
            {
                var newitemId = await _repository.postGuestReturnId(bk);
                if (newitemId != null)
                {
                    return Ok(newitemId);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region update Guest
        [HttpPut("{id}")]
        public async Task<ActionResult<Guest>> putot(int id, Guest bk)
        {
            if (ModelState.IsValid)
            {
                var updateitem = await _repository.putGuest(id, bk);
                if (updateitem != null)
                {
                    return Ok(updateitem);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region  7  - Delete a Guest
        [HttpDelete("{id}")]
        public IActionResult DeleteaGuest(int id)
        {
            try
            {
                var result = _repository.DeleteGuest(id);

                if (result == null)
                {

                    return NotFound(new
                    {
                        success = false,
                        message = "Guest could not be deleted or not found"
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
    }
}
