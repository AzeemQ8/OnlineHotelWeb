using OnlineHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
       
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
       
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public decimal Price { get; set; }
        public int TotalDays { get; set; }
        public OrderDetailViewModel()
        {

        }
        public OrderDetailViewModel(OrderDetail model)
        {
            Id = model.Id;
            OrderHeaderId = model.OrderHeaderId;
            RoomId = model.RoomId;
            Room = model.Room;
            Price = model.Price;
            //Count = model.Count;
            TotalDays = model.TotalDays;


        }
        public OrderDetail ConvertOrderDetailViewModel(OrderDetailViewModel model)
        {
            return new OrderDetail
            {

            };

        }
    }
}
