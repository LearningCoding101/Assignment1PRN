using FUMiniHotelSystem.model;
using FUMiniHotelSystem.repository;

namespace FUMiniHotelSystem.service
{
    public class AuthenticationService
    {
        private readonly IAuthenticateRepository _authenticateRepository;

        public AuthenticationService(IAuthenticateRepository authenticateRepository)
        {
            _authenticateRepository = authenticateRepository;
        }

        public Customer? AuthenticateUser(string email, string password)
        {
            return _authenticateRepository.AuthenticateUser(email, password);
        }

        public string GetUserRole(string email)
        {
            var customer = _authenticateRepository.AuthenticateUser(email, string.Empty);
            return customer?.EmailAddress == "admin@gmail.com" ? "Admin" : "Customer";
        }

        public IEnumerable<Customer> SearchCustomer(string keyword)
        {
            var customers = _authenticateRepository.GetAllCustomers();
            return customers.Where(c => c.EmailAddress.Contains(keyword) || c.CustomerFullName.Contains(keyword)).ToList();
        }

        public void RegisterUser(Customer customer)
        {
            _authenticateRepository.CreateUser(customer);
        }
    }
}
