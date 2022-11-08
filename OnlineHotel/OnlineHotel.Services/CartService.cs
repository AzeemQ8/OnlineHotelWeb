using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineHotel.BLL.Repository;
using OnlineHotel.Models;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;

namespace OnlineHotel.Services
{
    public class CartService : ICartService
    {
        private IUnitOfWork _unitOfWork;
        ILogger<FacilityService> _iLogger;

        public CartService(IUnitOfWork unitOfWork, ILogger<FacilityService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        private List<CartViewModel> ConvertModelToViewModelList(List<Cart> carts)
        {
            return carts.Select(x => new CartViewModel(x)).ToList();
        }
        private List<Cart> ConvertViewModellList(List<CartViewModel> carts)
        {
            return carts.Select(x => new CartViewModel().ConvertViewModel(x)).ToList();
        }

        public FacilitiesViewModel GetFacility(int Id)
        {
        var facility = _unitOfWork.GenericRepository<Facilities>().GetById(Id);
        var vm = new FacilitiesViewModel(facility);
        return vm;
        }

        public void UpdateFacility(FacilitiesViewModel facility)
        {
            var model = new FacilitiesViewModel().ConvertViewModel(facility);
            _unitOfWork.GenericRepository<Facilities>().Update(model);
            _unitOfWork.Save();
        }

        

        public void DeleteFacility(int id)
        {
            var facility = _unitOfWork.GenericRepository<Facilities>().GetById(id);

            _unitOfWork.GenericRepository<Facilities>().Delete(facility);
            _unitOfWork.Save();
        }

        //PagedResult<FacilitiesViewModel> IFacilityService.GetAll(int pageNumber, int PageSize)
        //{
        //    var vm = new FacilitiesViewModel();
        //    int totalCount;
        //    List<FacilitiesViewModel> vmList = new List<FacilitiesViewModel>();
        //    try
        //    {
        //        int ExcludeRecords = (PageSize * pageNumber) - PageSize;


        //        var facility = _unitOfWork.GenericRepository<Facilities>().GetAll()
        //            .Skip(ExcludeRecords).Take(PageSize).ToList();

        //        totalCount = _unitOfWork.GenericRepository<Facilities>().GetAll().ToList().Count;

        //        vmList = ConvertModelToViewModelList(facility);



        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    var result = new PagedResult<FacilitiesViewModel>
        //    {
        //        Data = vmList,
        //        TotalItems = totalCount,
        //        PageNumber = pageNumber,
        //        PageSize = PageSize

        //    };
        //    return result;
        //}

        //public IEnumerable<FacilitiesViewModel> GetAllFacilities()
        //{
            
        //    List<FacilitiesViewModel> vmList = new List<FacilitiesViewModel>();
        //    try
        //    {
                


        //        var facility = _unitOfWork.GenericRepository<Facilities>().GetAll()
        //            .ToList();

              

        //        vmList = ConvertModelToViewModelList(facility);



        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return vmList;
        //}

        //public List<SelectListItem> GetSelectedFacility(IEnumerable<int> selectedId)
        //{
        //  var list =   _unitOfWork.GenericRepository<Facilities>().GetAll().Select(x => new SelectListItem
        //    {
        //        Text = x.Title,
        //        Value = x.Id.ToString(),
        //        Selected = selectedId.Contains(x.Id)


        //    }).ToList();
        //    return list;
        //}

        public PagedResult<CartViewModel> GetAll(int pageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public CartViewModel GetCartById(int Id)
        {
            var model = _unitOfWork.GenericRepository<Cart>().GetById(Id);
            var vm = new CartViewModel(model);
            return vm;

        }

        public void UpdateCart(CartViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void InsertCart(CartViewModel viewModel)
        {
            var model = new CartViewModel().ConvertViewModel(viewModel);
            _unitOfWork.GenericRepository<Cart>().Add(model);
            _unitOfWork.Save();
        }

        public void DeleteCart(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CartViewModel> GetAllCartByUserId(string Id)
        {
          var model =   _unitOfWork.GenericRepository<Cart>().GetAll(x => x.CustomerId == Id, include: a => a.Include(s => s.Room));
            var vm = ConvertModelToViewModelList(model.ToList());
            return vm;
        }

        public void UpdateTotalDays(CartViewModel item, int days)
        {
            var model = new CartViewModel().ConvertViewModel(item);
           var cart =  _unitOfWork.GenericRepository<Cart>().GetByIdAsync(x=>x.Id==model.Id,include:x=>x.Include(s=>s.Room),disabledTracking:false);
            cart.TotalDays = days;
            cart.TotalPrice = (cart.Room.Price) * days;
            if (days == 1)
            {
                cart.ToDate = cart.FromDate;
            }
            else
            { 
            var todate = cart.FromDate.AddDays(days - 1);
            cart.ToDate = todate;
            }
            _unitOfWork.GenericRepository<Cart>().Update(cart);
            _unitOfWork.Save();
        }

        public void DeleteRange(List<CartViewModel> viewModel)
        {
          var listofCart = ConvertViewModellList(viewModel);
            _unitOfWork.GenericRepository<Cart>().DeleteRange(listofCart);
            _unitOfWork.Save();
        }
    }
}
