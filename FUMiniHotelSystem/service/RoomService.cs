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
        private readonly BookingReservationRepository _bookingReservationRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;

        public RoomService(RoomRepository roomRepository, RoomTypeRepository roomTypeRepository,
                           BookingReservationRepository bookingReservationRepository,
                           BookingDetailRepository bookingDetailRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _bookingReservationRepository = bookingReservationRepository;
            _bookingDetailRepository = bookingDetailRepository;
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


        public void BookRoom(int roomId, DateTime startDate, DateTime endDate, decimal actualPrice, int customerId)
        {
            // Create booking reservation
            var bookingReservation = new BookingReservation
            {
                BookingDate = DateTime.Now, 
                TotalPrice = actualPrice,   
                CustomerId = customerId,
                BookingStatus = 1           
            };

            _bookingReservationRepository.Add(bookingReservation);

            // Create booking detail
            var bookingDetail = new BookingDetail
            {
                BookingReservationID = bookingReservation.BookingReservationID,
                RoomID = roomId,
                StartDate = startDate,
                EndDate = endDate,
                ActualPrice = actualPrice
            };

            _bookingDetailRepository.Add(bookingDetail);
        }

        public void UpdateBooking(int bookingReservationId, DateTime startDate, DateTime endDate, decimal actualPrice)
        {
            var existingReservation = _bookingReservationRepository.GetById(bookingReservationId);
            if (existingReservation == null)
            {
                throw new InvalidOperationException("Booking reservation not found.");
            }

            var bookingDetail = _bookingDetailRepository.GetById(bookingReservationId);
            if (bookingDetail == null)
            {
                throw new InvalidOperationException("Booking detail not found.");
            }

            bookingDetail.StartDate = startDate;
            bookingDetail.EndDate = endDate;
            bookingDetail.ActualPrice = actualPrice;

            _bookingDetailRepository.Update(bookingDetail);
        }

        public void CancelBooking(int bookingReservationId)
        {

            _bookingDetailRepository.Delete(bookingReservationId);

            _bookingReservationRepository.Delete(bookingReservationId);
        }

        public BookingReservation GetBookingById(int bookingReservationId)
        {
            return _bookingReservationRepository.GetById(bookingReservationId);
        }

        public List<BookingReservation> GetAllBookings()
        {
            return _bookingReservationRepository.GetAll();
        }
    }
}
