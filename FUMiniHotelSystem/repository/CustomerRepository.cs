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

        string query = "SELECT CustomerID, CustomerFullname, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password " +
                       "FROM Customer";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Customer customer = new Customer {
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            CustomerFullName = reader.IsDBNull(reader.GetOrdinal("CustomerFullname")) ? null : reader.GetString(reader.GetOrdinal("CustomerFullname")),
                            Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                            EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                            CustomerBirthday = reader.IsDBNull(reader.GetOrdinal("CustomerBirthday")) ? null : reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                            CustomerStatus = reader.IsDBNull(reader.GetOrdinal("CustomerStatus")) ? null : reader.GetByte(reader.GetOrdinal("CustomerStatus")),
                            Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password"))
                        };

                        customers.Add(customer);
                    }
                }
            }
        }

        return customers;
    }

    public Customer GetById(int customerId) {
        Customer customer = null;

        string query = "SELECT CustomerID, CustomerFullname, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password " +
                       "FROM Customer WHERE CustomerID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerID", customerId);

                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        customer = new Customer {
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            CustomerFullName = reader.IsDBNull(reader.GetOrdinal("CustomerFullname")) ? null : reader.GetString(reader.GetOrdinal("CustomerFullname")),
                            Telephone = reader.IsDBNull(reader.GetOrdinal("Telephone")) ? null : reader.GetString(reader.GetOrdinal("Telephone")),
                            EmailAddress = reader.GetString(reader.GetOrdinal("EmailAddress")),
                            CustomerBirthday = reader.IsDBNull(reader.GetOrdinal("CustomerBirthday")) ? null : reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                            CustomerStatus = reader.IsDBNull(reader.GetOrdinal("CustomerStatus")) ? null : reader.GetByte(reader.GetOrdinal("CustomerStatus")),
                            Password = reader.IsDBNull(reader.GetOrdinal("Password")) ? null : reader.GetString(reader.GetOrdinal("Password"))
                        };
                    }
                }
            }
        }

        return customer;
    }

    public void Add(Customer customer) {
        string query = "INSERT INTO Customer (CustomerFullname, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) " +
                       "VALUES (?, ?, ?, ?, ?, ?)";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerFullname", customer.CustomerFullName ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@Password", customer.Password ?? (object) DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(Customer customer) {
        string query = "UPDATE Customer SET CustomerFullname = ?, Telephone = ?, EmailAddress = ?, " +
                       "CustomerBirthday = ?, CustomerStatus = ?, Password = ? WHERE CustomerID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@CustomerFullname", customer.CustomerFullName ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@Password", customer.Password ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int customerId) {
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
