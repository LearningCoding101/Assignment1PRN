using FUMiniHotelSystem;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

public class BookingDetailRepository : IRepository<BookingDetail>
{
    private readonly string _connectionString;

    public BookingDetailRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<BookingDetail> GetAll()
    {
        var bookingDetails = new List<BookingDetail>();

        string query = "SELECT BookingReservationID, RoomID, StartDate, EndDate, ActualPrice FROM BookingDetail";

        using (var connection = new OdbcConnection(_connectionString))
        {
            using (var command = new OdbcCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bookingDetail = new BookingDetail
                        {
                            BookingReservationID = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                            RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                            StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            ActualPrice = reader.IsDBNull(reader.GetOrdinal("ActualPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualPrice"))
                        };

                        bookingDetails.Add(bookingDetail);
                    }
                }
            }
        }

        return bookingDetails;
    }

    public BookingDetail GetById(int id)
    {
        BookingDetail bookingDetail = null;

        string query = "SELECT BookingReservationID, RoomID, StartDate, EndDate, ActualPrice FROM BookingDetail WHERE BookingReservationID = ?";

        using (var connection = new OdbcConnection(_connectionString))
        {
            using (var command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bookingDetail = new BookingDetail
                        {
                            BookingReservationID = reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                            RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                            StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("EndDate")),
                            ActualPrice = reader.IsDBNull(reader.GetOrdinal("ActualPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("ActualPrice"))
                        };
                    }
                }
            }
        }

        return bookingDetail;
    }

    public void Add(BookingDetail bookingDetail)
    {
        string query = "INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDate, EndDate, ActualPrice) VALUES (?, ?, ?, ?, ?)";

        using (var connection = new OdbcConnection(_connectionString))
        {
            using (var command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", bookingDetail.BookingReservationID);
                command.Parameters.AddWithValue("@RoomID", bookingDetail.RoomID);
                command.Parameters.AddWithValue("@StartDate", bookingDetail.StartDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EndDate", bookingDetail.EndDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ActualPrice", bookingDetail.ActualPrice ?? (object)DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(BookingDetail bookingDetail)
    {
        string query = "UPDATE BookingDetail SET StartDate = ?, EndDate = ?, ActualPrice = ? WHERE BookingReservationID = ? AND RoomID = ?";

        using (var connection = new OdbcConnection(_connectionString))
        {
            using (var command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@StartDate", bookingDetail.StartDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EndDate", bookingDetail.EndDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ActualPrice", bookingDetail.ActualPrice ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BookingReservationID", bookingDetail.BookingReservationID);
                command.Parameters.AddWithValue("@RoomID", bookingDetail.RoomID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        string query = "DELETE FROM BookingDetail WHERE BookingReservationID = ?";

        using (var connection = new OdbcConnection(_connectionString))
        {
            using (var command = new OdbcCommand(query, connection))
            {
                command.Parameters.AddWithValue("@BookingReservationID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
