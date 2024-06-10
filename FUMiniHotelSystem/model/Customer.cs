using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.model
{
    internal class Customer
    {
        private long customerID;
        private string? fullName;
        private string? phone;
        private string? email;
        private DateTime birthday;
        private bool status;
        private string password = "";
        public Customer()
        {
            
        }
        public Customer(long customerID,
            string? fullName,
            string? phone,
            string? email,
            DateTime birthday,
            bool status,
            string password)
        {
            this.customerID = customerID;
            this.fullName = fullName;
            this.phone = phone;
            this.email = email;
            this.birthday = birthday;
            this.status = status;
            this.password = password;
        }
        
        public long CustomerID { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public bool Status { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            string customerDetails = $"CustomerID: {CustomerID}, ";
            customerDetails += $"FullName: {(Fullname ?? "N/A")}, "; 
            customerDetails += $"Phone: {(Phone ?? "N/A")}, ";       
            customerDetails += $"Email: {(Email ?? "N/A")}, ";      
            customerDetails += $"Birthday: {Birthday}, ";
            customerDetails += $"Status: {Status}, ";
            customerDetails += $"Password: {(Password ?? "N/A")}";   
            return customerDetails;
        }

    }

}
