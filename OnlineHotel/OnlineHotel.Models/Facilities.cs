using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Models
{
    public class Facilities
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<RoomFacilities> RoomFacilities { get; set; }

    }
}
