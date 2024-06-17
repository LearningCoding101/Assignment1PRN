using FUMiniHotelSystem;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

public class RoomRepository : IRepository<Room> {
    private readonly string _connectionString;

    public RoomRepository(string connectionString) {
        _connectionString = connectionString;
    }

    internal List<Room> GetAll() {
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
                            RoomDetailDescription = reader.IsDBNull(reader.GetOrdinal("RoomDetailDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomDetailDescription")),
                            RoomMaxCapacity = reader.IsDBNull(reader.GetOrdinal("RoomMaxCapacity")) ? null : reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                            RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                            RoomStatus = reader.IsDBNull(reader.GetOrdinal("RoomStatus")) ? null : reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                            RoomPricePerDay = reader.IsDBNull(reader.GetOrdinal("RoomPricePerDay")) ? null : reader.GetDecimal(reader.GetOrdinal("RoomPricePerDay"))
                        };

                        rooms.Add(room);
                    }
                }
            }
        }

        return rooms;
    }

    internal Room GetById(int roomId) {
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
                            RoomDetailDescription = reader.IsDBNull(reader.GetOrdinal("RoomDetailDescription")) ? null : reader.GetString(reader.GetOrdinal("RoomDetailDescription")),
                            RoomMaxCapacity = reader.IsDBNull(reader.GetOrdinal("RoomMaxCapacity")) ? null : reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                            RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                            RoomStatus = reader.IsDBNull(reader.GetOrdinal("RoomStatus")) ? null : reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                            RoomPricePerDay = reader.IsDBNull(reader.GetOrdinal("RoomPricePerDay")) ? null : reader.GetDecimal(reader.GetOrdinal("RoomPricePerDay"))
                        };
                    }
                }
            }
        }

        return room;
    }

    internal void Add(Room room) {
        string query = "INSERT INTO Room (RoomNumber, RoomDetailDescription, RoomMaxCapacity, RoomTypeID, RoomStatus, RoomPricePerDay) " +
                       "VALUES (?, ?, ?, ?, ?, ?)";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@RoomDetailDescription", room.RoomDetailDescription ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomMaxCapacity", room.RoomMaxCapacity ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomPricePerDay", room.RoomPricePerDay ?? (object) DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal void Update(Room room) {
        string query = "UPDATE Room SET RoomNumber = ?, RoomDetailDescription = ?, RoomMaxCapacity = ?, " +
                       "RoomTypeID = ?, RoomStatus = ?, RoomPricePerDay = ?  WHERE RoomID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                command.Parameters.AddWithValue("@RoomDetailDescription", room.RoomDetailDescription ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomMaxCapacity", room.RoomMaxCapacity ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                command.Parameters.AddWithValue("@RoomStatus", room.RoomStatus ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomPricePerDay", room.RoomPricePerDay ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomID", room.RoomID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal void Delete(int roomId) {
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
