using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public interface IOrderHeaderService
    {
        OrderHeaderViewModel GetHeaderById(int Id);
        void InsertOrder(OrderHeaderViewModel viewModel);
        void Update(OrderHeaderViewModel orderHeader);
        void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null);
        void PaymentStatus(int Id, string SessionId, string PaymentIntentId);
    }
}
