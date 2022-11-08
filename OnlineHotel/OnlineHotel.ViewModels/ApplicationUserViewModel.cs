using OnlineHotel.Models;

namespace OnlineHotel.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string? City { get; set; }
        
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }




        public ApplicationUserViewModel()
        {

        }
        public ApplicationUserViewModel(ApplicationUser model)
        {
            Id = model.Id;
            Name = model.Name;
            Phone = model.PhoneNumber;
            Email = model.Email;
            City = model.City;           
            State= model.State;
            PostalCode= model.PostalCode;
            Address = model.Address;

        }
        public ApplicationUser ConvertViewModel(ApplicationUserViewModel model)
        {
            return new ApplicationUser {
                Id = model.Id,
            Name = model.Name,
            PhoneNumber = model.Phone,
            Email = model.Email,
            City=model.City,
            State=model.State,
            PostalCode=model.PostalCode,
            Address=model.Address
            



                                        };

        }
    }
}
