using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.dto
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public RoomTypeDTO? RoomType { get; set; }
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }
    }
}
