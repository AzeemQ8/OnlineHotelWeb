using OnlineHotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Services
{
    public interface IApplicationUserService
    {
        ApplicationUserViewModel GetUserById(string Id);
    }
}
