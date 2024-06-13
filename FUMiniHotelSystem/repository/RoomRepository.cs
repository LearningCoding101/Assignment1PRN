using FUMiniHotelSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

public class RoomRepository : IRepository<Room> {
    private readonly string _connectionString;

    public RoomRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public List<Room> GetAll() {
        List<Room> rooms = new List<Room>();

        string query = "SELECT RoomID, RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDay FROM Room";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Room room = new Room {
                            RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                            RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                            RoomDetailDescription = reader.GetString(reader.GetOrdinal("RoomDescription")),
                            RoomMaxCapacity = reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                            RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                            RoomStatus = reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                            RoomPricePerDay = reader.GetDecimal(reader.GetOrdinal("RoomPricePerDate"))
                        };

                        rooms.Add(room);
                    }
                }
            }
        }

        return rooms;
    }

    public Room GetById(int roomId) {
        Room room = null;

        string query = "SELECT RoomID, RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDate " +
                       "FROM Room WHERE RoomID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomID", roomId);

                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        room = new Room {
                            RoomID = reader.GetInt32(reader.GetOrdinal("RoomID")),
                            RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber")),
                            RoomDetailDescription = reader.GetString(reader.GetOrdinal("RoomDescription")),
                            RoomMaxCapacity = reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                            RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                            RoomStatus = reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                            RoomPricePerDay = reader.GetDecimal(reader.GetOrdinal("RoomPricePerDate"))
                        };
                    }
                }
            }
        }

        return room;
    }

    public void Add(Room room) {
        string query = "INSERT INTO Room (RoomNumber, RoomDescription, RoomMaxCapacity, RoomStatus, RoomPricePerDate, RoomTypeID) " +
                       "VALUES (?, ?, ?, ?, ?, ?)";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@RoomDescription", room.RoomDescription);
                command.Parameters.AddWithValue("@RoomMaxCapacity", room.RoomMaxCapacity);
                command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus);
                command.Parameters.AddWithValue("@RoomPricePerDate", room.RoomPricePerDate);
                command.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(Room room) {
        string query = "UPDATE Room SET RoomNumber = ?, RoomDescription = ?, RoomMaxCapacity = ?, " +
                       "RoomStatus = ?, RoomPricePerDate = ?, RoomTypeID = ? WHERE RoomID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@RoomDescription", room.RoomDescription);
                command.Parameters.AddWithValue("@RoomMaxCapacity", room.RoomMaxCapacity);
                command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus);
                command.Parameters.AddWithValue("@RoomPricePerDate", room.RoomPricePerDate);
                command.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                command.Parameters.AddWithValue("@RoomID", room.RoomID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int roomId) {
        string query = "DELETE FROM Room WHERE RoomID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomID", roomId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
