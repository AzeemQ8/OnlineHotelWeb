using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineHotel.BLL;
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
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork;
        ILogger<RoomService> _iLogger;
        private ApplicationDbContext _context;

        public RoomService(IUnitOfWork unitOfWork, ILogger<RoomService> iLogger, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
            _context = context;
        }

        public void DeleteRoom(int id)
        {
            var room = _unitOfWork.GenericRepository<Room>().GetById(id);

            _unitOfWork.GenericRepository<Room>().Delete(room);
            _unitOfWork.Save();
        }

        public PagedResult<RoomViewModel> GetAll(int pageNumber, int PageSize)
        {
            var vm = new RoomViewModel();
            int totalCount;
            List<RoomViewModel> vmList = new List<RoomViewModel>();
            try
            {
                int ExcludeRecords = (PageSize * pageNumber) - PageSize;


                var rooms = _unitOfWork.GenericRepository<Room>().
                    GetAll(include:x=>x.Include(a=>a.RoomType)
                    .Include(z=>z.RoomFacilities).ThenInclude(f=>f.Facilities))

                    .Skip(ExcludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Room>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(rooms);



            }
            catch (Exception)
            {

                throw;
            }
            var result = new PagedResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = PageSize

            };
            return result;
        }

        private List<RoomViewModel> ConvertModelToViewModelList(List<Room> rooms)
        {
            return rooms.Select(x => new RoomViewModel(x)).ToList();
        }

        public RoomViewModel GetRoom(int Id)
        {
            var room = _unitOfWork.GenericRepository<Room>().GetByIdAsync(x=>x.Id==Id,include:y=>y.
            Include(z=>z.RoomType).Include(d=>d.RoomFacilities).ThenInclude(g=>g.Facilities));
            var vm = new RoomViewModel(room);
            return vm;
        }

        public void InsertRoom(RoomViewModel room, List<string> facilities)
        {
            var model = new RoomViewModel().ConvertModel(room);
            foreach (var item in facilities)
            {
                model.RoomFacilities.Add(new RoomFacilities()
                {
                    FacilitiesId = int.Parse(item)
                });
            }
          
           
            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModel room)
        {
            var roomFromDb = _unitOfWork.GenericRepository<Room>().GetById(room.Id);          
           var roomfromDatabase = _unitOfWork.GenericRepository<Room>().GetByIdAsync(x => x.Id == room.Id, include: y => y
           .Include(d => d.RoomFacilities),disabledTracking:false);
            var existingIds = roomfromDatabase.RoomFacilities.Select(x => x.FacilitiesId).ToList();
            var selectedIds = room.facilities.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();
            var toAdd = selectedIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectedIds).ToList();
            roomFromDb.RoomFacilities = roomfromDatabase.RoomFacilities.Where(x => !toRemove.Contains(x.FacilitiesId)).ToList();
            foreach (var item in toAdd)
            {
                roomFromDb.RoomFacilities.Add(new RoomFacilities()
                {
                    FacilitiesId = item
                   

                });
               
            }
            roomFromDb.RoomNumber=room.RoomNumber;
            roomFromDb.Status=room.Status;
            roomFromDb.RoomType=room.RoomType;
            roomFromDb.Price = room.RoomPrice;
            _unitOfWork.GenericRepository<Room>().Update(roomFromDb);
            _unitOfWork.Save();
          
        }

        public IEnumerable<RoomViewModel> GetAllRoom()
        {
           
            
            List<RoomViewModel> vmList = new List<RoomViewModel>();
            try
            {
               
                var rooms = _unitOfWork.GenericRepository<Room>().GetAll()
                    .ToList();
                vmList = ConvertModelToViewModelList(rooms);
            }
            catch (Exception)
            {

                throw;
            }
            return vmList;
        }

        public IEnumerable<int> GetFacilitiesId(int id)
        {
            var room = _unitOfWork.GenericRepository<Room>().GetByIdAsync(x => x.Id == id, include: y => y.
            Include(z => z.RoomType).Include(d => d.RoomFacilities).ThenInclude(g => g.Facilities));
            var selectedIds =    room.RoomFacilities.Select(x => x.FacilitiesId).ToList();
            return selectedIds;
        }
    }
}
