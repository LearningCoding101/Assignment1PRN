using FUMiniHotelSystem;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

public class BookingReservationRepository : IRepository<BookingReservation>
{
    private readonly string _connectionString;

    public BookingReservationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<BookingReservation> GetAll()
    {
        List<BookingReservation> bookingReservations = new List<BookingReservation>();

        string query = "SELECT BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus FROM BookingReservation";

        using (OdbcConnection connection = new OdbcConnection(_connectionString))
        {
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BookingReservation bookingReservation = new BookingReservation
                        {
                            BookingReservationID = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                            BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                            TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            BookingStatus = reader.IsDBNull(reader.GetOrdinal("BookingStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("BookingStatus"))
                        };

                        bookingReservations.Add(bookingReservation);
                    }
                }
            }
        }

        return bookingReservations;
    }

    public BookingReservation GetById(int bookingReservationId)
    {
        BookingReservation bookingReservation = null;

        string query = "SELECT BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus FROM BookingReservation WHERE BookingReservationID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString))
        {
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", bookingReservationId);

                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bookingReservation = new BookingReservation
                        {
                            BookingReservationID = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                            BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                            TotalPrice = reader.IsDBNull(reader.GetOrdinal("TotalPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                            BookingStatus = reader.IsDBNull(reader.GetOrdinal("BookingStatus")) ? (byte?)null : reader.GetByte(reader.GetOrdinal("BookingStatus"))
                        };
                    }
                }
            }
        }

        return bookingReservation;
    }

    public List<BookingReservation> GetReservationsByCustomerId(int customerId)
    {
        var list = new List<BookingReservation>();
        using( var connection = new OdbcConnection(_connectionString))
        {
            var query = "SELECT * FROM BookingReservation Where CustomerId = ?";
            var command = new OdbcCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerId", customerId);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new BookingReservation
                    {
                        BookingReservationID = reader.GetInt32(0),
                        BookingDate = reader.GetDateTime(1),
                        TotalPrice = reader.GetDecimal(2),
                        CustomerId = reader.GetInt32(3),
                        BookingStatus = reader.GetByte(4),
                    });
                }
            }
        }
        return list;
    }

    public void Add(BookingReservation bookingReservation)
    {
        string query = "INSERT INTO BookingReservation (BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus) VALUES (?, ?, ?, ?, ?)";

        using (OdbcConnection connection = new OdbcConnection(_connectionString))
        {
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", bookingReservation.BookingReservationID);
                command.Parameters.AddWithValue("@BookingDate", bookingReservation.BookingDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TotalPrice", bookingReservation.TotalPrice ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CustomerID", bookingReservation.CustomerId);
                command.Parameters.AddWithValue("@BookingStatus", bookingReservation.BookingStatus ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(BookingReservation bookingReservation)
    {
        string query = "Update BookingReservation SET BookingDate = ?, TotalPrice = ?, CustomerID = ?, BookingStatus = ? WHERE BookingReservationID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString))
        {
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingDate", bookingReservation.BookingDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TotalPrice", bookingReservation.TotalPrice ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CustomerID", bookingReservation.CustomerId);
                command.Parameters.AddWithValue("@BookingStatus", bookingReservation.BookingStatus ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BookingReservationID", bookingReservation.BookingReservationID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int bookingReservationId)
    {
        string query = "DELETE FROM BookingReservation WHERE BookingReservationID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString))
        {
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", bookingReservationId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
