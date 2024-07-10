using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using FUMiniHotelSystem.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.service
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public List<CustomerDTO> GetAllCustomers()
        {
            return _customerRepository.GetAll()
                .Select(CustomerMapper.MapCustomerToCustomerDTO)
                .ToList();
        }

        public CustomerDTO GetCustomerById(int id)
        {
            return CustomerMapper.MapCustomerToCustomerDTO(_customerRepository.GetById(id));
        }

        public void AddCustomer(CustomerDTO customer) {
            _customerRepository.Add(CustomerMapper.MapCustomerDTOToCustomer(customer));
        }
        public void UpdateCustomer(CustomerDTO customer)
        {
            _customerRepository.Update(CustomerMapper.MapCustomerDTOToCustomer(customer));
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.Delete(id);
        }

        public CustomerDTO? SearchCustomer(string keyword)
        {
            return _customerRepository.GetAll()
                .Select(CustomerMapper.MapCustomerToCustomerDTO)
                .FirstOrDefault(c =>
                    (!string.IsNullOrEmpty(c.CustomerFullName) && c.CustomerFullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(c.EmailAddress) && c.EmailAddress.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0));
        }

    }
}
