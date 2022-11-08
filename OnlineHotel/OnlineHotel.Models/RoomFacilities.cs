using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Models
{
    public class RoomFacilities
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        
        public Room Room { get; set; }
        public int FacilitiesId { get; set; }
      
        public Facilities Facilities { get; set; }
    }
}
