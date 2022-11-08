using Microsoft.Extensions.Logging;
using OnlineHotel.BLL.Repository;
using OnlineHotel.Models;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IUnitOfWork _unitOfWork;
        ILogger<FacilityService> _iLogger;

        public ApplicationUserService(IUnitOfWork unitOfWork,
            ILogger<FacilityService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public ApplicationUserViewModel GetUserById(string Id)
        {
         var model =   _unitOfWork.GenericRepository<ApplicationUser>().GetByIdAsync(x=>x.Id==Id);
            var vm = new ApplicationUserViewModel(model);
            return vm;
        }



    }
}
