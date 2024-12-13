using HotelManagementNew.Models;
using HotelManagementNew.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementNew.Repository
{
    public interface IBookingRepository
    {
        #region 1 -  Get all Bookings from DB 
        public Task<ActionResult<IEnumerable<Booking>>> GetTblBookings();
        #endregion

        #region   2 - Get an Bookings based on Id
        public Task<ActionResult<Booking>> GetBookingsById(int id);
        #endregion

        #region  3  - Insert an Bookings -return Bookings record
        public Task<ActionResult<Booking>> PostBookingReturnRecord(Booking book);
        #endregion

        #region  4  - Update an Bookings with ID
        public Task<ActionResult<Booking>> PutTblBooking(int id, Booking book);
        #endregion

        #region 5  - Delete an Bookings
        public JsonResult DeleteBooking(int id);
        #endregion

        #region  6 - Get all GusestBooking Details 
        public Task<ActionResult<IEnumerable<GuestBookingHistory>>> GetVMGuestBookingHistory();
        #endregion

        #region  7 - Get all CurrentBooking Details 
        public Task<ActionResult<IEnumerable<CurrentBookings>>> GetVMCurrentBookings();
        #endregion

        #region   8 - call stored procedure -- View Booking Details
        //public Task<List<GuestReportViewModel>> GetGuestReportById(int guestId);
        #endregion
    }
}
