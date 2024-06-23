using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.utility
{
    public class RoomMapper
    {
        public static RoomDTO MapRoomToRoomDTO(Room room, RoomType roomType)
        {
            return new RoomDTO
            {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                RoomDetailDescription = room.RoomDetailDescription,
                RoomMaxCapacity = room.RoomMaxCapacity,
                RoomType = MapRoomTypeToRoomTypeDTO(roomType),
                RoomStatus = room.RoomStatus,
                RoomPricePerDay = room.RoomPricePerDay
            };
        }

        public static Room MapRoomDTOToRoom(RoomDTO roomDTO)
        {
            return new Room
            {
                RoomID = roomDTO.RoomID,
                RoomNumber = roomDTO.RoomNumber,
                RoomDetailDescription = roomDTO.RoomDetailDescription,
                RoomMaxCapacity = roomDTO.RoomMaxCapacity,
                RoomTypeID = roomDTO.RoomType.RoomTypeID,
                RoomStatus = roomDTO.RoomStatus,
                RoomPricePerDay = roomDTO.RoomPricePerDay
            };
        }

        public static RoomTypeDTO MapRoomTypeToRoomTypeDTO(RoomType roomType)
        {
            return new RoomTypeDTO
            {
                RoomTypeID = roomType.RoomTypeID,
                RoomTypeName = roomType.RoomTypeName,
                TypeDescription = roomType.TypeDescription,
                TypeNote = roomType.TypeNote
            };
        }

        public static RoomType MapRoomTypeDTOToRoomType(RoomTypeDTO roomTypeDTO)
        {
            return new RoomType
            {
                RoomTypeID = roomTypeDTO.RoomTypeID,
                RoomTypeName = roomTypeDTO.RoomTypeName,
                TypeDescription = roomTypeDTO.TypeDescription,
                TypeNote = roomTypeDTO.TypeNote
            };
        }

    }
}
