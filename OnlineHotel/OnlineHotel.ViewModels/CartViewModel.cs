using OnlineHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string CustomerId { get; set; }
        
        public ApplicationUser Customer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDays { get; set; }
        public List<CartViewModel> ListofCart { get; set; }
        public OrderHeaderViewModel OrderHeader { get; set; }
        public CartViewModel()
        {

        }
        public CartViewModel(Cart cart)
        {
            Id = cart.Id;           
            RoomId = cart.RoomId;
            Room = cart.Room;
            CustomerId = cart.CustomerId;
            FromDate = cart.FromDate;
            ToDate = cart.ToDate;
            TotalDays = cart.TotalDays;
            TotalPrice = cart.TotalPrice;
            TotalDays = cart.TotalDays;

        }
        public Cart ConvertViewModel(CartViewModel cart)
        {
            return new Cart
            {
                Id = cart.Id,
               
                RoomId = cart.RoomId,
                CustomerId = cart.CustomerId,
                FromDate = cart.FromDate,
                ToDate = cart.ToDate,
                TotalDays = cart.TotalDays,
                TotalPrice = cart.TotalPrice,

            };
         
            

        }
    }
}
