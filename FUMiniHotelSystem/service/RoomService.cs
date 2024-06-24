using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using FUMiniHotelSystem.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<RoomDTO> GetAllRoom()
        {
            List<Room> room = _roomRepository.GetAll();
            List<RoomDTO> result = new List<RoomDTO>();
            for(int i = 0; i < room.Count; i++)
            {
                RoomType roomType = _roomTypeRepository.GetById(room[i].RoomTypeID);
                result.Add(RoomMapper.MapRoomToRoomDTO(room[i],
                    roomType)
                    );
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
            _roomTypeRepository.Update(RoomMapper
                .MapRoomTypeDTOToRoomType(room.RoomType)
                );
        }

        public void DeleteRoom(int id)
        {
            _roomRepository.Delete(id);
        }

        public List<RoomDTO> SearchRoom(string keyword)
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
    }
}
