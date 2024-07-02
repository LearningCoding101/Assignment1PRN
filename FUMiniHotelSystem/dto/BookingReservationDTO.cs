using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.dto
{
    public class BookingReservationDTO
    {
        public int BookingReservationID { get; set; }
        public DateTime? BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte? BookingStatus { get; set; }
    }
}

