using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineHotel.Utility;
using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public interface ICartService
    {
        PagedResult<CartViewModel> GetAll(int pageNumber, int PageSize);
        CartViewModel GetCartById(int Id);
       
         IEnumerable<CartViewModel> GetAllCartByUserId(string id);
        void UpdateCart(CartViewModel viewModel);
        void InsertCart(CartViewModel viewModel);
        void DeleteCart(int id);
        void DeleteRange(List<CartViewModel> viewModel);
        void UpdateTotalDays(CartViewModel item, int days);
    }
}
