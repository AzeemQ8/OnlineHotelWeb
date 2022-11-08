using Microsoft.Extensions.Logging;
using OnlineHotel.BLL.Repository;
using OnlineHotel.Models;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private IUnitOfWork _unitOfWork;
        ILogger<OrderDetailService> _iLogger;
        public void InsertOrder(OrderDetailViewModel viewModel)
        {
            var model = new OrderDetailViewModel().ConvertOrderDetailViewModel(viewModel);
            _unitOfWork.GenericRepository<OrderDetail>().Add(model);
            _unitOfWork.Save();
        }
    }
}
