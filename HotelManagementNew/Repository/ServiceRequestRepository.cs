using HotelManagementNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementNew.Repository
{
    


    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly HotelMgntDemoContext _context;

        public ServiceRequestRepository(HotelMgntDemoContext context)
        {
            _context = context;
        }
        public JsonResult DeleteService(int id)
        {
            try
            {//check idf employee object is not null
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid service id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }
                // ensure context is not null
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database coontext is not initialized"

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                //Find the employee by id
                var existingServices = _context.ServiceRequests.Find(id);
                if (existingServices == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "ServiceRequests  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove

                _context.ServiceRequests.Remove(existingServices);



                //save changes to the database
                _context.SaveChangesAsync();


                return new JsonResult(new
                {
                    success = true,
                    message = "Services deleted successfully"

                })
                {
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database coontext is not initialized"

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetAllServices()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.ServiceRequests.ToListAsync();
                }

                
                return new List<ServiceRequest>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest serviceRequest)
        {
            try
            {
                // Validate the input object
                if (serviceRequest == null)
                {
                    throw new ArgumentNullException(nameof(serviceRequest), "ServiceRequest data is null");
                }

                // Ensure the database context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                // Call the stored procedure to insert a new service request
                var parameters = new[]
                {
            new SqlParameter("@BookingId", serviceRequest.BookingId),
            new SqlParameter("@ServiceId", serviceRequest.ServiceId),
            new SqlParameter("@RequestDate", serviceRequest.RequestDate)
        };

                await _context.Database.ExecuteSqlRawAsync("EXEC RequestAdditionalServices @BookingId, @ServiceId, @RequestDate", parameters);

                // Optionally, retrieve the newly added service request to confirm
                var createdRequest = await _context.ServiceRequests
                    .FirstOrDefaultAsync(sr =>
                        sr.BookingId == serviceRequest.BookingId &&
                        sr.ServiceId == serviceRequest.ServiceId &&
                        sr.RequestDate == serviceRequest.RequestDate);

                return createdRequest;
            }
            catch (Exception ex)
            {
                // Log the exception (if a logging mechanism is in place)
                Console.WriteLine(ex.Message);
                return null; // Or return a meaningful error response as per your API design
            }
        }

    }
}
