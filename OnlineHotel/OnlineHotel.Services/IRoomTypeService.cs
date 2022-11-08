using OnlineHotel.Utility;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public interface IRoomTypeService
    {
        PagedResult<RoomTypeViewModel> GetAllRoomTypes(int pageNumber,int pageSize);
        IEnumerable<RoomTypeViewModel> AllRoomTypes();
        RoomTypeViewModel GetRoomType(int TypeId);
        void UpdateRoomType(RoomTypeViewModel roomType);
        void InsertRoomType(RoomTypeViewModel roomType);
        void DeleteRoomType(int id);


    }
}
