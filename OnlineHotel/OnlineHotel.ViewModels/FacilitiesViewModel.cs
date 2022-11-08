using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.ViewModels
{
    public class FacilitiesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        

        public FacilitiesViewModel()
        {

        }
        public FacilitiesViewModel(Facilities model)
        {
            Id = model.Id;
            Title = model.Title;
           

        }
        public Facilities ConvertViewModel(FacilitiesViewModel model)
        {
            return new Facilities
            {
                Id = model.Id,
                Title = model.Title
                
        };
        }
    }
}
