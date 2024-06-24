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
            List<CustomerDTO> result = new List<CustomerDTO>();

            List<Customer> customerList = _customerRepository.GetAll();
            for(int i = 0; i < customerList.Count; i++)
            {
                result.Add(CustomerMapper.MapCustomerToCustomerDTO(customerList[i]));
            }

            return result;
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
            List<Customer> customers = _customerRepository.GetAll();

            foreach (var customer in customers)
            {
                CustomerDTO customerDTO = CustomerMapper.MapCustomerToCustomerDTO(customer);

                if ((!string.IsNullOrEmpty(customerDTO.CustomerFullName) && customerDTO.CustomerFullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(customerDTO.EmailAddress) && customerDTO.EmailAddress.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    return customerDTO; 
                }
            }

            return null; 
        }

    }
}
