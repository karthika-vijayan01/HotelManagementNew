using HotelManagementNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementNew.Repository
{
    public interface IServiceRequestRepository
    {
        #region 1-Get all Services
        public Task<ActionResult<IEnumerable<ServiceRequest>>> GetAllServices();

        #endregion
        #region  2-Addd new services
        public Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest serviceRequest);
        #endregion
        #region 3- Delete Service
        public JsonResult DeleteService(int id);
        #endregion
    }
}
