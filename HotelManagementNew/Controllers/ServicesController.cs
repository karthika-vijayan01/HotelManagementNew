using HotelManagementNew.Models;
using HotelManagementNew.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagementNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepository _repository;
        //DI -- constructor injection
        public ServicesController(IServiceRepository repository)
        {
            _repository = repository;
        }
        #region search all employees
        [HttpGet]
       

        public async Task<ActionResult<IEnumerable<Service>>> GetAllservices()
        {
            var service = await _repository.GetAllServices();
            if (service == null)
            {
                return NotFound("No service found ");
            }
            return Ok(service);
        }
        #endregion
        #region 4 insert 
        [HttpPost]
        public async Task<ActionResult<Service>> InsertTblEmployeesReturnRecord(Service ser)
        {
            if (ModelState.IsValid)
            {
                var newser = await _repository.postService(ser);
                if (newser != null)
                {
                    return Ok(newser);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region update employee
        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> putTblEmployee(int id, Service ser)
        {
            if (ModelState.IsValid)
            {
                var updateser = await _repository.putservice(id, ser);
                if (updateser != null)
                {
                    return Ok(updateser);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region  7  - Delete services
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            try
            {
                var result = _repository.DeleteService(id);

                if (result == null)
                {
                    //if result indicates failure or null
                    return NotFound(new
                    {
                        success = false,
                        message = "services could not be deleted or not found"
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
