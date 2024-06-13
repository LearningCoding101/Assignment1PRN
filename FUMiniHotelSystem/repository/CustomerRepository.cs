using FUMiniHotelSystem;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

public class CustomerRepository : IRepository<Customer> {
    private readonly string _connectionString;

    public CustomerRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public List<Customer> GetAll() {
        List<Customer> customers = new List<Customer>();

        string query = "SELECT CustomerID, CustomerFullname, Telephone, EmailAddress, CustomerBirthday, CustomerStatus " +
                       "FROM Customer";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Customer customer = new Customer {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            CustomerFullname = reader.GetString(reader.GetOrdinal("CustomerFullname")),
                            Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                            EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                            CustomerBirthday = reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                            CustomerStatus = reader.GetInt32(reader.GetOrdinal("CustomerStatus"))
                        };

                        customers.Add(customer);
                    }
                }
            }
        }

        return customers;
    }

    public Customer GetCustomerById(int customerId) {
        Customer customer = null;

        string query = "SELECT CustomerID, CustomerFullname, Telephone, EmailAddress, CustomerBirthday, CustomerStatus " +
                       "FROM Customer WHERE CustomerID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerID", customerId);

                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        customer = new Customer {
                            CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            CustomerFullname = reader.GetString(reader.GetOrdinal("CustomerFullname")),
                            Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                            EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                            CustomerBirthday = reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                            CustomerStatus = reader.GetInt32(reader.GetOrdinal("CustomerStatus"))
                        };
                    }
                }
            }
        }

        return customer;
    }

    public void AddCustomer(Customer customer) {
        string query = "INSERT INTO Customer (CustomerFullname, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) " +
                       "VALUES (?, ?, ?, ?, ?, ?)";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerFullname", customer.CustomerFullname);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus);
                command.Parameters.AddWithValue("@Password", customer.Password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateCustomer(Customer customer) {
        string query = "UPDATE Customer SET CustomerFullname = ?, Telephone = ?, EmailAddress = ?, " +
                       "CustomerBirthday = ?, CustomerStatus = ?, Password = ? WHERE CustomerID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerFullname", customer.CustomerFullname);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus);
                command.Parameters.AddWithValue("@Password", customer.Password);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteCustomer(int customerId) {
        string query = "DELETE FROM Customer WHERE CustomerID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerID", customerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
