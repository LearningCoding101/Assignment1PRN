using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.utility
{
    public class CustomerMapper
    {
        public static CustomerDTO MapCustomerToCustomerDTO(Customer customer)
        {
            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                CustomerFullName = customer.CustomerFullName,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress,
                CustomerBirthday = customer.CustomerBirthday,
                CustomerStatus = customer.CustomerStatus,
                Password = customer.Password
            };
        }
        public static Customer MapCustomerDTOToCustomer(CustomerDTO customer)
        {
            return new Customer
            {
                CustomerId = customer.CustomerId,
                CustomerFullName = customer.CustomerFullName,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress,
                CustomerBirthday = customer.CustomerBirthday,
                CustomerStatus = customer.CustomerStatus,
                Password = customer.Password
            };
        }
    }
}
