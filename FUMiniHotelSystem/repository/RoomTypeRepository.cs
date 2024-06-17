using FUMiniHotelSystem;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

public class RoomTypeRepository : IRepository<RoomType>{
    private readonly string _connectionString;

    public RoomTypeRepository(string connectionString) {
        _connectionString = connectionString;
    }

    internal List<RoomType> GetAll() {
        List<RoomType> roomTypes = new List<RoomType>();

        string query = "SELECT RoomTypeID, RoomTypeName, TypeDescription, TypeNote FROM RoomType";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        RoomType roomType = new RoomType {
                            RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                            RoomTypeName = reader.GetString(reader.GetOrdinal("RoomTypeName")),
                            TypeDescription = reader.IsDBNull(reader.GetOrdinal("TypeDescription")) ? null : reader.GetString(reader.GetOrdinal("TypeDescription")),
                            TypeNote = reader.IsDBNull(reader.GetOrdinal("TypeNote")) ? null : reader.GetString(reader.GetOrdinal("TypeNote"))
                        };

                        roomTypes.Add(roomType);
                    }
                }
            }
        }

        return roomTypes;
    }

    internal RoomType GetById(int roomTypeId) {
        RoomType roomType = null;

        string query = "SELECT RoomTypeID, RoomTypeName, TypeDescription, TypeNote " +
                       "FROM RoomType WHERE RoomTypeID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomTypeID", roomTypeId);

                connection.Open();
                using (OdbcDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        roomType = new RoomType {
                            RoomTypeID = reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                            RoomTypeName = reader.GetString(reader.GetOrdinal("RoomTypeName")),
                            TypeDescription = reader.IsDBNull(reader.GetOrdinal("TypeDescription")) ? null : reader.GetString(reader.GetOrdinal("TypeDescription")),
                            TypeNote = reader.IsDBNull(reader.GetOrdinal("TypeNote")) ? null : reader.GetString(reader.GetOrdinal("TypeNote"))
                        };
                    }
                }
            }
        }

        return roomType;
    }

    internal void Add(RoomType roomType) {
        string query = "INSERT INTO RoomType (RoomTypeName, TypeDescription, TypeNote) " +
                       "VALUES (?, ?, ?)";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomTypeName", roomType.RoomTypeName);
                command.Parameters.AddWithValue("@TypeDescription", roomType.TypeDescription ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@TypeNote", roomType.TypeNote ?? (object) DBNull.Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal void Update(RoomType roomType) {
        string query = "UPDATE RoomType SET RoomTypeName = ?, TypeDescription = ?, TypeNote = ? " +
                       "WHERE RoomTypeID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomTypeName", roomType.RoomTypeName);
                command.Parameters.AddWithValue("@TypeDescription", roomType.TypeDescription ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@TypeNote", roomType.TypeNote ?? (object) DBNull.Value);
                command.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    internal void Delete(int roomTypeId) {
        string query = "DELETE FROM RoomType WHERE RoomTypeID = ?";

        using (OdbcConnection connection = new OdbcConnection(_connectionString)) {
            using (OdbcCommand command = new OdbcCommand(query, connection)) {
                command.Parameters.AddWithValue("@RoomTypeID", roomTypeId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
