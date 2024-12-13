using HotelManagementNew.Models;
using HotelManagementNew.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HotelManagementNew.Repository
{
    public interface IServiceRepository
    {
        #region 1-Get all Services
        public Task<ActionResult<IEnumerable<Service>>> GetAllServices();

        #endregion
        #region 2-Get all services by viewmodel
        public Task<ActionResult<IEnumerable<ServiceViewModel>>> GetViewModeLservice();
        #endregion
        #region  3-Addd new services
        public Task<ActionResult<Service>> postService(Service service);
        #endregion
        #region 4- Delete Service
        public JsonResult DeleteService(int id);
        #endregion
        #region 6 get employees by its id
        public Task<ActionResult<Service>> putservice(int id, Service service);
        #endregion 

    }
}
