using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using FUMiniHotelSystem.repository;
using FUMiniHotelSystem.utility;

namespace FUMiniHotelSystem.service
{
    public class AuthenticationService
    {
        private readonly IAuthenticateRepository _authenticateRepository;
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "123";

        public AuthenticationService(IAuthenticateRepository authenticateRepository)
        {
            _authenticateRepository = authenticateRepository;
        }

        public CustomerDTO? AuthenticateUser(LoginRequestDTO loginRequest)
        {
            if (loginRequest.EmailAddress == AdminEmail && loginRequest.Password == AdminPassword)
            {
                return new CustomerDTO
                {
                    CustomerId = -1,
                    CustomerFullName = "Admin",
                    EmailAddress = AdminEmail,
                    CustomerStatus = 1 
                };
            }

            var user = _authenticateRepository.AuthenticateUser(loginRequest.EmailAddress, loginRequest.Password);
            return user != null ? CustomerMapper.MapCustomerToCustomerDTO(user) : null;
        }

        public string GetUserRole(string email)
        {
            return email == AdminEmail ? "Admin" : "Customer";
        }

        public IEnumerable<CustomerDTO> SearchCustomer(string keyword)
        {
            var customers = _authenticateRepository.GetAllCustomers();
            return customers
                .Where(c => c.EmailAddress.Contains(keyword) || c.CustomerFullName.Contains(keyword))
                .Select(CustomerMapper.MapCustomerToCustomerDTO)
                .ToList();
        }

        public void RegisterUser(CustomerDTO customerDTO)
        {
            var customer = CustomerMapper.MapCustomerDTOToCustomer(customerDTO);
            _authenticateRepository.CreateUser(customer);
        }
    }
}