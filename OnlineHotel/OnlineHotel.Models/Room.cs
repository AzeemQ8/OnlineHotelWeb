using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
  
        public RoomType RoomType { get; set; }
        public RoomAvail Status { get; set; }
        public decimal Price { get; set; }
        public string RoomPicture { get; set; }

      
        public ICollection<RoomFacilities> RoomFacilities { get; set; }=new HashSet<RoomFacilities>();
  
    }
    public enum RoomAvail:int
    {   
        Booked=0,
        Available=1
    }
}
