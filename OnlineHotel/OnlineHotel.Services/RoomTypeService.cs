using Microsoft.Extensions.Logging;
using OnlineHotel.BLL.Repository;
using OnlineHotel.Models;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private IUnitOfWork _unitOfWork;
        ILogger<RoomTypeService> _iLogger;

        public RoomTypeService(IUnitOfWork unitOfWork, ILogger<RoomTypeService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public void DeleteRoomType(int id)
        {
            var roomType = _unitOfWork.GenericRepository<RoomType>().GetById(id);
            
            _unitOfWork.GenericRepository<RoomType>().Delete(roomType);
            _unitOfWork.Save();
        }

        public PagedResult<RoomTypeViewModel> GetAllRoomTypes(int pageNumber,int PageSize)
        {
            var vm = new RoomTypeViewModel();
            int totalCount;
            List<RoomTypeViewModel> vmList = new List<RoomTypeViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * pageNumber) - PageSize;
                

                var roomTypes = _unitOfWork.GenericRepository<RoomType>().GetAll()
                    .Skip(ExcludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<RoomType>().GetAll().ToList().Count;
                
                vmList = ConvertModelToViewModelList(roomTypes);
                


            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<RoomTypeViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = PageSize

            };
            return result;


                                
        }

        public IEnumerable<RoomTypeViewModel> AllRoomTypes()
        {
          
            List<RoomTypeViewModel> vmList = new List<RoomTypeViewModel>();
            try
            {
                
                var roomTypes = _unitOfWork.GenericRepository<RoomType>().GetAll()
                    .ToList();

                vmList = ConvertModelToViewModelList(roomTypes);



            }
            catch (Exception)
            {

                throw;
            }
            return vmList;

        }

        public RoomTypeViewModel GetRoomType(int TypeId)
        {
            var roomType = _unitOfWork.GenericRepository<RoomType>().GetById(TypeId);
            var vm = new RoomTypeViewModel(roomType);
            return vm;
        }

        public void InsertRoomType(RoomTypeViewModel roomType)
        {
            var model = new RoomTypeViewModel().ConvertModel(roomType);
            _unitOfWork.GenericRepository<RoomType>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoomType(RoomTypeViewModel roomType)
        {
            var model =  new RoomTypeViewModel().ConvertModel(roomType);
            _unitOfWork.GenericRepository<RoomType>().Update(model);
            _unitOfWork.Save();
        }

        private List<RoomTypeViewModel> ConvertModelToViewModelList(IEnumerable<RoomType> roomTypes)
        {
            return roomTypes.Select(x => new RoomTypeViewModel(x)).ToList();
        }
    }
}
