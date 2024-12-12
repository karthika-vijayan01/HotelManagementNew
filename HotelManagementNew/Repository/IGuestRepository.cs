using HotelManagementNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementNew.Repository
{
    public interface IGuestRepository
    {
        #region 1-Get all Guest
        public Task<ActionResult<IEnumerable<Guest>>> GetAllGuest();
        #endregion
        #region 2-Get Guest by id 
        public Task<ActionResult<Guest>> GetGuestById(int id);
        #endregion
        #region  3-insert Guest
        public Task<ActionResult<Guest>> postGuestReturnRecord(Guest guest);
        #endregion
        #region 4 insert all Guest
        public Task<ActionResult<int>> postGuestReturnId(Guest guest);
        #endregion 
        #region 6 get Guest by its id
        public Task<ActionResult<Guest>> putGuest(int id, Guest guest);
        #endregion
        #region 7-delete Guest
        public JsonResult DeleteGuest(int id);
        #endregion
    }
}
