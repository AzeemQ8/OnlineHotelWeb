using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineHotel.Services;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;

namespace OnlineHotel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacilitiesController : Controller
    {
       private IFacilityService _facilityService;
        private IRoomService _roomService;

        public FacilitiesController(IFacilityService facilityService, IRoomService roomService)
        {
            _facilityService = facilityService;
            _roomService = roomService;
        }

        public IActionResult Index(int pageNumner = 1, int pageSize = 10)
        {
            PagedResult<FacilitiesViewModel> vm = _facilityService.GetAll(pageNumner, pageSize);
            return View(vm);
            
        }

        public IActionResult Edit(int id)
        {
            var viewModel = _facilityService.GetFacility(id);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(FacilitiesViewModel vm)
        {
            _facilityService.UpdateFacility(vm);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Create()
        {
           
            ViewBag.Room = new SelectList(_roomService.GetAllRoom(), "Id", "RoomNumber");
            return View();
        }
        [HttpPost]

        public IActionResult Create(FacilitiesViewModel vm)
        {


            _facilityService.InsertFacility(vm);
                return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            _facilityService.DeleteFacility(id);
            return RedirectToAction("Index");
        }
    }
}
