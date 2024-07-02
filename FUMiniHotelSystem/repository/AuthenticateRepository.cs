using FUMiniHotelSystem.model;
using FUMiniHotelSystem.service;
using System.Data.Odbc;

namespace FUMiniHotelSystem.repository
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly string _connectionString;

        public AuthenticateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Customer? AuthenticateUser(string email, string password)
        {
            using (var connection = new OdbcConnection(_connectionString))
            {
                var query = "SELECT * FROM Customer WHERE EmailAddress = ?";
                var command = new OdbcCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var storedPassword = reader.GetString(6);
                        if(PasswordHasher.VerifyPassword(password, storedPassword))
                        {
                            return new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                CustomerFullName = reader.GetString(1),
                                Telephone = reader.GetString(2),
                                EmailAddress = reader.GetString(3),
                                CustomerBirthday = reader.GetDateTime(4),
                                CustomerStatus = reader.GetByte(5),
                                Password = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            using (var connection = new OdbcConnection(_connectionString))
            {
                var query = "SELECT * FROM Customer";
                var command = new OdbcCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = reader.GetInt32(0),
                            CustomerFullName = reader.GetString(1),
                            Telephone = reader.GetString(2),
                            EmailAddress = reader.GetString(3),
                            CustomerBirthday = reader.GetDateTime(4),
                            CustomerStatus = reader.GetByte(5),
                            Password = reader.GetString(6)
                        });
                    }
                }
            }
            return customers;
        }

        public void CreateUser(Customer customer)
        {
            using(var connection = new OdbcConnection(_connectionString))
            {
                var query = "INSERT INTO Customer (CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) "
                    + "VALUES (?, ?, ?, ?, ?, ?)";
                var command = new OdbcCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus);
                command.Parameters.AddWithValue("@Password", PasswordHasher.HashPassword(customer.Password));

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
