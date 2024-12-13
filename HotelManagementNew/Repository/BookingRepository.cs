using HotelManagementNew.Models;
using HotelManagementNew.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementNew.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelMgntDemoContext _context;

        public BookingRepository(HotelMgntDemoContext context)
        {
            _context = context; 
        }

        #region   1 -  Get all Bookings from DB 
        public async Task<ActionResult<IEnumerable<Booking>>> GetTblBookings()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.Bookings.Include(book => book.Guest).Include(book => book.Room).ToListAsync();
                }
                return new List<Booking>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  2 - Get an Bookings based on Id
        public async Task<ActionResult<Booking>> GetBookingsById(int id)
        {
            try
            {
                if (_context != null)
                {
                    //var tblEmployees = await _context.TblEmployees.FFindAsync(id);
                    var book = await _context.Bookings.Include(book => book.Guest).Include(book => book.Room).FirstOrDefaultAsync(b => b.BookingId == id);
                    return book;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  3  - Insert an Bookings -return Bookings record
        public async Task<ActionResult<Booking>> PostBookingReturnRecord(Booking book)
        {
            try
            {
                if (book == null)
                {
                    throw new ArgumentException(nameof(book), "Booking data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.Bookings.AddAsync(book);

                await _context.SaveChangesAsync();

                var bookingDetail = await _context.Bookings.Include(b => b.Guest).Include(b => b.Room)
                    .FirstOrDefaultAsync(b => b.BookingId == book.BookingId);

                return bookingDetail;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  4  - Update an Bookings with ID
        public async Task<ActionResult<Booking>> PutTblBooking(int id, Booking book)
        {
            try
            {
                if (book == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                var existingBooking = await _context.Bookings.FindAsync(id);
                if (existingBooking == null)
                {
                    return null;
                }

                existingBooking.GuestId = book.GuestId;
                existingBooking.RoomId = book.RoomId;
                existingBooking.BookingDate = book.BookingDate;
                existingBooking.CheckInDate = book.CheckInDate;
                existingBooking.CheckOutDate = book.CheckOutDate;
                existingBooking.TotalAmount = book.TotalAmount;
                
                await _context.SaveChangesAsync();

                var bookingRecord = await _context.Bookings.Include(book => book.Guest).Include(book => book.Room)
                    .FirstOrDefaultAsync(existingBooking => existingBooking.BookingId == book.BookingId);

                return bookingRecord;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 5  - Delete an Bookings
        public JsonResult DeleteBooking(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid Booking Id"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
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
                var existingBook = _context.Bookings.Find(id);

                if (existingBook == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Booking not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                _context.Bookings.Remove(existingBook);

                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "Booking Deleted successfully"
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

            #endregion

            #region  6 - Get all GusestBooking Details
            public async Task<ActionResult<IEnumerable<GuestBookingHistory>>> GetVMGuestBookingHistory()
            {
            //LINQ
            try
            {
                if (_context != null)
                {

                    return await (from b in _context.Bookings
                                  join r in _context.Rooms on b.RoomId equals r.RoomId
                                  where b.CheckOutDate < DateTime.Now 
                                  select new GuestBookingHistory
                                  {
                                      RoomNo = r.RoomNo,
                                      CheckInDate = b.CheckInDate,
                                      CheckOutDate = b.CheckOutDate
                                  }).ToListAsync();

                }
                //Returns an empty list if context is null
                return new List<GuestBookingHistory>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  7 - Get all CurrentBooking Details 
        public async Task<ActionResult<IEnumerable<CurrentBookings>>> GetVMCurrentBookings()
        {
            //LINQ
            try
            {
                if (_context != null)
                {

                    return await (from b in _context.Bookings
                                  join g in _context.Guests on b.GuestId equals g.GuestId
                                  join r in _context.Rooms on b.RoomId equals r.RoomId
                                  where b.CheckInDate <= DateTime.Now && b.CheckOutDate >= DateTime.Now 
                                  select new CurrentBookings
                                  {
                                      GuestName = g.GuestName,
                                      RoomNo = r.RoomNo,
                                      CheckInDate = b.CheckInDate
                                  }).ToListAsync();


                }
                //Returns an empty list if context is null
                return new List<CurrentBookings>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        //public async Task<List<GuestReportViewModel>> GetGuestReportById(int guestId)
        //{
        //    try
        //    {
        //        if (_context == null)
        //            throw new InvalidOperationException("Database context is not initialized");

        //        var result = await _context.Set<GuestReportViewModel>()
        //            .FromSqlRaw("EXEC GetGuestReport1 @GuestId", new SqlParameter("@GuestId", guestId))
        //            .ToListAsync();

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception here if needed
        //        throw new Exception("Error fetching guest report", ex);
        //    }
        //}

    }
}
