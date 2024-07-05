using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using FUMiniHotelSystem.utility;
using System;
using System.Collections.Generic;

namespace FUMiniHotelSystem.service
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;
        private readonly RoomTypeRepository _roomTypeRepository;
        

        public RoomService(RoomRepository roomRepository, RoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            
        }

        public List<RoomDTO> GetAllRooms()
        {
            List<Room> rooms = _roomRepository.GetAll();
            List<RoomDTO> result = new List<RoomDTO>();
            foreach (var room in rooms)
            {
                RoomType roomType = _roomTypeRepository.GetById(room.RoomTypeID);
                result.Add(RoomMapper.MapRoomToRoomDTO(room, roomType));
            }
            return result;
        }

        public RoomDTO GetRoomById(int id)
        {
            Room room = _roomRepository.GetById(id);
            RoomType roomType = _roomTypeRepository.GetById(room.RoomTypeID);
            return RoomMapper.MapRoomToRoomDTO(room, roomType);
        }

        public void UpdateRoom(RoomDTO room)
        {
            _roomRepository.Update(RoomMapper.MapRoomDTOToRoom(room));
            _roomTypeRepository.Update(RoomMapper.MapRoomTypeDTOToRoomType(room.RoomType));
        }

        public void DeleteRoom(int id)
        {
            _roomRepository.Delete(id);
        }
        public void AddRoom(RoomDTO room)
        {
            var newRoom = RoomMapper.MapRoomDTOToRoom(room);
            _roomRepository.Add(newRoom);
        }
        public List<RoomDTO> SearchRooms(string keyword)
        {
            List<Room> rooms = _roomRepository.GetAll();
            List<RoomDTO> result = new List<RoomDTO>();

            foreach (var room in rooms)
            {
                RoomType roomType = _roomTypeRepository.GetById(room.RoomTypeID);
                RoomDTO roomDTO = RoomMapper.MapRoomToRoomDTO(room, roomType);

                if ((!string.IsNullOrEmpty(roomDTO.RoomNumber) && roomDTO.RoomNumber.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(roomDTO.RoomDetailDescription) && roomDTO.RoomDetailDescription.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    result.Add(roomDTO);
                }
            }

            return result;
        }
        public List<RoomTypeDTO> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = _roomTypeRepository.GetAll();
            List<RoomTypeDTO> result = new List<RoomTypeDTO>();

            foreach (var roomType in roomTypes)
            {
                result.Add(RoomMapper.MapRoomTypeToRoomTypeDTO(roomType));
            }

            return result;
        }


    }
}
