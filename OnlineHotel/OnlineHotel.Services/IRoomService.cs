using OnlineHotel.Utility;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public interface IRoomService
    {
        PagedResult<RoomViewModel> GetAll(int pageNumber, int PageSize);
        IEnumerable<RoomViewModel> GetAllRoom();
        RoomViewModel GetRoom(int Id);
        void UpdateRoom(RoomViewModel room);
        void InsertRoom(RoomViewModel room,List<string> facilities);
        IEnumerable<int> GetFacilitiesId(int id);
        void DeleteRoom(int id);






    }
}
