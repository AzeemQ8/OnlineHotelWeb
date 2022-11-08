using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineHotel.Services;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;
using System.Security.Claims;

namespace OnlineHotel.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
   
    public class HomeController : Controller
    {
        private IRoomService _roomService;
        private IRoomTypeService _roomTypeService;
        private IFacilityService _facilityService;
        private ICartService _cartService;
        public HomeController(IRoomService roomService,
            IRoomTypeService roomTypeService
,
            ICartService cartService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _cartService = cartService;
        }


        public IActionResult Index(int pageNumber = 1, int pageSize = 8)
        {

           PagedResult<RoomViewModel> vm = _roomService.GetAll(pageNumber, pageSize);
        
            return View(vm);
        }
        [HttpGet]
        public IActionResult RoomDetails(int roomId)
        {
            List<SelectListItem> item = new List<SelectListItem>();
            for (int i = 1; i <=10; i++)
            {
                item.Add(new SelectListItem
                {
                    Text =i.ToString(),
                    Value=i.ToString()
                            });
            }
            ViewBag.selectedDays = new SelectList(item, "Value", "Text");

            var vm = _roomService.GetRoom(roomId);
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public IActionResult RoomDetails(RoomViewModel vm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

           
            CartViewModel cart = new CartViewModel();
           
            var roomViewModel = _roomService.GetRoom(vm.Id);

             cart.Room = new RoomViewModel().ConvertModel(roomViewModel);
            cart.CustomerId = claims.Value;
            cart.FromDate = vm.FromDate;
            if (vm.TotalDays==1)
            {
                cart.ToDate = vm.FromDate;
            }
            else
            { 
            var todate = vm.FromDate.AddDays(vm.TotalDays - 1);
            cart.ToDate = todate;
            }
            cart.RoomId = vm.Id;
             cart.TotalDays = vm.TotalDays;
            cart.TotalPrice = (roomViewModel.RoomPrice) * (vm.TotalDays);
           
            _cartService.InsertCart(cart);  
            return RedirectToAction("Index","Home");
        }
    }
}
