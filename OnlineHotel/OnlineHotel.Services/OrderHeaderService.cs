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
    public class OrderHeaderService : IOrderHeaderService
    {
        private IUnitOfWork _unitOfWork;
        ILogger<OrderHeaderService> _iLogger;

        public OrderHeaderService(IUnitOfWork unitOfWork, 
            ILogger<OrderHeaderService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public OrderHeaderViewModel GetHeaderById(int Id)
        {
            var OrderHeader = _unitOfWork.GenericRepository<OrderHeader>().GetById(Id);
            var vm = new OrderHeaderViewModel(OrderHeader);
            return vm;
        }

        public void InsertOrder(OrderHeaderViewModel viewModel)
        {
            var model = new OrderHeaderViewModel().ConvertOrderHeaderViewModel(viewModel);
            _unitOfWork.GenericRepository<OrderHeader>().Add(model);
            _unitOfWork.Save();
        }

        public void PaymentStatus(int Id, string SessionId, string PaymentIntentId)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderHeaderViewModel orderHeader)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null)
        {
            throw new NotImplementedException();
        }
    }
}
