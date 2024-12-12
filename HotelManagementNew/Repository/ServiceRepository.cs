using HotelManagementNew.Models;
using HotelManagementNew.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementNew.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HotelMgntDemoContext _context;
        //DI --constructor injection
        //field injection(one of the types)
        //constructor name 
        // context  
        public ServiceRepository(HotelMgntDemoContext context)
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
                var existingServices = _context.Services.Find(id);
                if (existingServices == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "service  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove

                _context.Services.Remove(existingServices);



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

        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Services.ToListAsync();
                }

                //return an empty list if context is null
                return new List<Service>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<ActionResult<IEnumerable<ServiceViewModel>>> GetViewModeLservice()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Service>> postService(Service service)
        {
            try
            {//check idf employee object is not null
                if (service == null)
                {
                    throw new ArgumentNullException(nameof(service), "service data is null");

                }
                // ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                //add the employee record to the dbcontext
                await _context.Services.AddAsync(service);

                //save changes to the database
                await _context.SaveChangesAsync();
                //retrive the employee with the related departement
                var servicebook = await _context.Services
                    .FirstOrDefaultAsync(e => e.ServiceId == service.ServiceId);
                return servicebook;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async  Task<ActionResult<Service>> putservice(int id, Service service)
        {
            try
            {//check idf employee object is not null
                if (service == null)
                {
                    throw new ArgumentNullException(nameof(service), "service data is null");

                }
                // ensure context is not null
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }
                //Find the employee by id
                var existingEmployee = await _context.Services.FindAsync(id);
                if (existingEmployee == null)
                {
                    return null;
                }

                //map values with fields - update
                existingEmployee.ServiceName = service.ServiceName;
                existingEmployee.ServicePrice = service.ServicePrice;
                



               
                await _context.SaveChangesAsync();
                
                var servicebook = await _context.Services
                    .FirstOrDefaultAsync(e => e.ServiceId == service.ServiceId);
                return servicebook;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
