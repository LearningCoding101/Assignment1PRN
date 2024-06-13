using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.model
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; } = null!;

        public string? Telephone { get; set; }

        public string EmailAddress { get; set; } = null!;

        public DateTime? CustomerBirthday { get; set; }

        public bool CustomerStatus {  get; set; }

        public string Password { get; set; } = null!;
    }
}
