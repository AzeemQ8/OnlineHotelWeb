using Microsoft.AspNetCore.Mvc;
using OnlineHotel.BLL;
using OnlineHotel.BLL.Repository;
using OnlineHotel.Models;
using OnlineHotel.Services;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;

namespace OnlineHotel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomTypeController : Controller
    {
        private IRoomTypeService _roomType;
       
        public RoomTypeController(IRoomTypeService roomType)
        {
            _roomType = roomType;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {

            PagedResult<RoomTypeViewModel> vm = _roomType.GetAllRoomTypes(pageNumber, pageSize);
            return View(vm);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _roomType.GetRoomType(id);
           return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(RoomTypeViewModel vm)
        {
            _roomType.UpdateRoomType(vm);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
       
        public IActionResult Create(RoomTypeViewModel vm)
        {
            _roomType.InsertRoomType(vm);
           return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
           _roomType.DeleteRoomType(id);
            return RedirectToAction("Index");
        }
    }
}
