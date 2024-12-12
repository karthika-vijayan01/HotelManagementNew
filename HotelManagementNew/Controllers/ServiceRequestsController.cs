using HotelManagementNew.Models;
using HotelManagementNew.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagementNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestRepository _repository;
        //DI -- constructor injection
        public ServiceRequestsController(IServiceRequestRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]


        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetAllservices()
        {
            var service = await _repository.GetAllServices();
            if (service == null)
            {
                return NotFound("No ServiceRequest found ");
            }
            return Ok(service);
        }
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
                        message = "ServiceRequest could not be deleted or not found"
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
        #region 4 insert 
        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> InsertServiceRequest(ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                // Call the repository method to insert the service request
                var newServiceRequest = await _repository.PostServiceRequest(serviceRequest);

                if (newServiceRequest != null)
                {
                    // Return the newly added service request object with an HTTP 200 status
                    return Ok(newServiceRequest);
                }
                else
                {
                    // Return an HTTP 404 status if the operation fails
                    return NotFound();
                }
            }

            // Return an HTTP 400 status if the model state is invalid
            return BadRequest();
        }
        #endregion

    }
}
