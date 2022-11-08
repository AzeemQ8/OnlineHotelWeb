using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineHotel.Models;
using OnlineHotel.Services;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;

namespace OnlineHotel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomsController : Controller
    {
        IWebHostEnvironment env;

        private IRoomService _roomService;
        private IRoomTypeService _roomTypeService;
        private IFacilityService _facilityService;


        public RoomsController(IRoomService roomService,
            IWebHostEnvironment env,
            IRoomTypeService roomTypeService,
            IFacilityService facilityService)
        {
            _roomService = roomService;
            this.env = env;
            _roomTypeService = roomTypeService;
            _facilityService = facilityService;
        }
        public IActionResult Index(int pageNumner = 1, int pageSize = 10)
        {
            PagedResult<RoomViewModel> vm = _roomService.GetAll(pageNumner, pageSize);
            return View(vm);
            
        }


        public IActionResult Edit(int id)
        {

            var viewModel = _roomService.GetRoom(id);
            ViewBag.type = new SelectList(_roomTypeService.AllRoomTypes(),"Id","Name",viewModel.RoomTypeId);
            var enumData = from RoomAvail e in Enum.GetValues(typeof(RoomAvail))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            ViewBag.RoomStatus = new SelectList(enumData.ToList(), "ID", "Name", viewModel.StatusId);
            var selectedId = _roomService.GetFacilitiesId(id);
            viewModel.facilities = _facilityService.GetSelectedFacility(selectedId);
            
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(RoomViewModel vm)
        {

            _roomService.UpdateRoom(vm);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Create()
        {
            RoomViewModel vm = new RoomViewModel();
            var facilities = _facilityService.GetAllFacilities().Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
          
            vm.facilities = facilities;
            ViewBag.RoomTypes = new SelectList(_roomTypeService.AllRoomTypes(), "Id", "Name");
            var enumData = from RoomAvail e in Enum.GetValues(typeof(RoomAvail))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            

            ViewBag.EnumList = new SelectList(enumData.ToList(), "ID", "Name");
            return View(vm);
        }
        [HttpPost]

        public IActionResult Create(RoomViewModel vm)
        {
            if (vm.RoomPictureFile!=null)
            {
                ImageOperations image = new ImageOperations(env);
                string ImageFileName = image.UploadImage(vm);
                vm.RoomPicture = ImageFileName;

            }
            else
            {
                vm.RoomPicture = "~/Images/Hoteldemo.jpg";
            }

            var selectedFacilities = vm.facilities.
                Where(x => x.Selected).Select(y => y.Value).ToList();
            
            _roomService.InsertRoom(vm,selectedFacilities);
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int id)
        {
            _roomService.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}
