using OnlineHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.ViewModels
{
    public class OrderHeaderViewModel
    {
        public int Id { get; set; }
        public decimal OrderTotal { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUserViewModel ApplicationUser { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string PostalCode { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime DateOfOrder { get; set; }
        public string? SessionId { get; set; }
        public OrderHeaderViewModel()
        {

        }
        public OrderHeaderViewModel(OrderHeader model)
        {
            Id = model.Id;
            OrderTotal = model.OrderTotal;
            CustomerId = model.CustomerId;
            OrderStatus = model.OrderStatus;
            PaymentStatus = model.PaymentStatus;
            DateOfOrder = model.DateOfOrder;
            SessionId = model.SessionId;
        }

        public OrderHeader ConvertOrderHeaderViewModel(OrderHeaderViewModel model)
        {

            return new OrderHeader
            {
                Id = model.Id,
                OrderTotal = model.OrderTotal,
                CustomerId = model.CustomerId,
                OrderStatus = model.OrderStatus,
                PaymentStatus = model.PaymentStatus,
                DateOfOrder = model.DateOfOrder,
                SessionId = model.SessionId
            };
        }

    }
}
