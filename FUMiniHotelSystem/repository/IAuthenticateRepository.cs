using FUMiniHotelSystem.model;

namespace FUMiniHotelSystem.repository
{
    public interface IAuthenticateRepository
    {
        Customer AuthenticateUser (string email, string password);

        IEnumerable<Customer> GetAllCustomers();

        void CreateUser(Customer customer);
    }
}
