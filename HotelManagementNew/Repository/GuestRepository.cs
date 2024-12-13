using HotelManagementNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementNew.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelMgntDemoContext _context;
        //DI --constructor injection
        //field injection(one of the types)
        //constructor name 
        // context  
        public GuestRepository(HotelMgntDemoContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Guest>>> GetAllGuest()
        {
            if (_context != null)
            {
                return await _context.Guests
                    .Include(b => b.Bookings)
                    .ToListAsync();
            }

            // Return an empty list if context is null
            return new List<Guest>();
        }

        public async Task<ActionResult<Guest>> GetGuestById(int id)
        {
            try
            {
                if (_context != null)
                {
                    // find the employee by id 
                    var guest = await _context.Guests
                    .Include(guest => guest.Bookings)
                 
                    .FirstOrDefaultAsync(e => e.GuestId == id);
                    return guest;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Guest>> postGuestReturnRecord(Guest guest)
        {
            try
            {
                if (guest == null)
                {
                    throw new ArgumentNullException(nameof(guest), "Guest data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                await _context.Guests.AddAsync(guest);


                await _context.SaveChangesAsync();

                var lib = await _context.Guests.Include(e => e.Bookings)
                    .FirstOrDefaultAsync(e => e.GuestId == guest.GuestId);
                return lib;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<int>> postGuestReturnId(Guest guest)
        {
            try
            {
                if (guest == null)
                {
                    throw new ArgumentNullException(nameof(guest), "Guest data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                await _context.Guests.AddAsync(guest);


                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return guest.GuestId;
                }
                else
                {
                    throw new Exception("failed to save Guest record to the database");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Guest>> putGuest(int id, Guest guest)
        {
            try
            {
                if (guest == null)
                {
                    throw new ArgumentNullException(nameof(guest), "Guest data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                var existingOrderItem = await _context.Guests.FindAsync(id);
                if (existingOrderItem == null)
                {
                    return null;
                }


                existingOrderItem.GuestName = guest.GuestName;
                existingOrderItem.ContactNumber = guest.ContactNumber;
                existingOrderItem.AadhaarNumber = guest.AadhaarNumber;





                await _context.SaveChangesAsync();

                var Bk = await _context.Guests.Include(e => e.Bookings)
                    .FirstOrDefaultAsync(e => e.GuestId == guest.GuestId);
                return Bk;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult DeleteGuest(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Guest id"

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
                var existingbook = _context.Guests.Find(id);
                if (existingbook == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Guest  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove

                _context.Guests.Remove(existingbook);



                //save changes to the database
                _context.SaveChangesAsync();


                return new JsonResult(new
                {
                    success = true,
                    message = "Guest deleted successfully"

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
                    message = "Database context is not initialized"

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
