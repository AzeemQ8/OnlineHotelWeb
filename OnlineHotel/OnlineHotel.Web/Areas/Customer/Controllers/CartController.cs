using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineHotel.Models;
using OnlineHotel.Services;
using OnlineHotel.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;

namespace OnlineHotel.Web.Areas.Customer.Controllers
{
    [Area("customer")]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IApplicationUserService _applicationUserService;
        private IOrderHeaderService _orderHeaderService;
        private IOrderDetailService _orderDetailService;
        public CartViewModel vm { get; set; }

        public CartController(ICartService cartService,
            IApplicationUserService applicationUserService,
            IOrderHeaderService orderHeaderService,
            IOrderDetailService orderDetailService)
        {
            _cartService = cartService;
            _applicationUserService = applicationUserService;
            _orderHeaderService = orderHeaderService;
            _orderDetailService = orderDetailService;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var vm = new CartViewModel()
            {
                ListofCart = _cartService.GetAllCartByUserId(claims.Value).ToList(),
                OrderHeader= new OrderHeaderViewModel()


            };
            foreach (var cart in vm.ListofCart)
            {
                vm.OrderHeader.OrderTotal += (cart.TotalPrice);
            }
            List<SelectListItem> item = new List<SelectListItem>();
            for (int i = 1; i <= 10; i++)
            {
                item.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            ViewBag.selectedDays = new SelectList(item, "Value", "Text",vm.TotalDays);


            return View(vm);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var vm = new CartViewModel()
            {
                ListofCart = _cartService.GetAllCartByUserId(claims.Value).ToList(),
                OrderHeader = new OrderHeaderViewModel()


            };
            vm.OrderHeader.ApplicationUser = _applicationUserService.GetUserById(claims.Value);
            vm.OrderHeader.Name = vm.OrderHeader.ApplicationUser.Name;
            vm.OrderHeader.Phone = vm.OrderHeader.ApplicationUser.Phone;
            vm.OrderHeader.Address = vm.OrderHeader.ApplicationUser.Address;


            
            foreach (var cart in vm.ListofCart)
            {
                vm.OrderHeader.OrderTotal += (cart.TotalPrice);
            }


            return View(vm);
        }
        public IActionResult ordersuccess(int id)
        {

            var orderHeader =_orderHeaderService.GetHeaderById(id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
            if (session.PaymentStatus.ToLower() == "paid")
            {
                _orderHeaderService.UpdateStatus(id,
                    "Approved", "Approved");
            }
            List<CartViewModel> cart = _cartService.GetAllCartByUserId(orderHeader.ApplicationUser.Id).ToList();
            _cartService.DeleteRange(cart);
            return View(id);

        }

        [HttpPost]
        public IActionResult Summary(CartViewModel vm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm.ListofCart =_cartService.GetAllCartByUserId(claims.Value).ToList();
            vm.OrderHeader.OrderStatus = "Pending";
            vm.OrderHeader.PaymentStatus = "Pending";
            vm.OrderHeader.DateOfOrder = DateTime.Now;
            vm.OrderHeader.CustomerId = claims.Value;

            foreach (var cart in vm.ListofCart)
            {
                vm.OrderHeader.OrderTotal += (cart.TotalPrice);
            }
            _orderHeaderService.InsertOrder(vm.OrderHeader);
           

            foreach (var item in vm.ListofCart)
            {
                OrderDetailViewModel orderDetail = new OrderDetailViewModel()
                {
                    RoomId = item.RoomId,
                    OrderHeaderId = vm.OrderHeader.Id,
                    //Count = item.Count,
                    TotalDays = item.TotalDays,
                    Price = item.Room.Price
                };
                _orderDetailService.InsertOrder(orderDetail);
          
            }

            // Card Details Here
            var domain = "https://localhost:7129/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"customer/cart/ordersuccess?id={vm.OrderHeader.Id}",
                CancelUrl = domain + $"customer/cart/Index",
            };

            foreach (var item in vm.ListofCart)
            {

                var lineItemsOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Room.Price * 100),
                        Currency = "INR",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Room.RoomNumber.ToString(),
                        },

                    },
                    Quantity = item.TotalDays,
                };
                options.LineItems.Add(lineItemsOptions);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            _orderHeaderService.PaymentStatus(vm.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _cartService.DeleteRange(vm.ListofCart);
             //extra code 

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);


            return RedirectToAction("Index", "home");



        }

        [HttpGet]
        public IActionResult UpdateDays(string mainId,string userSelect)
        {
            int Id = Convert.ToInt32(mainId);
            int days = Convert.ToInt32(userSelect);
            var item =  _cartService.GetCartById(Id);
            _cartService.UpdateTotalDays(item, days);
            return RedirectToAction("Index");
        }


    }
}
