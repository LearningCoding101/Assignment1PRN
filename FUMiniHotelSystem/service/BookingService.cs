using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;

namespace FUMiniHotelSystem.service
{
    public class BookingService
    {
        private readonly BookingReservationRepository _bookingReservationRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;

        public BookingService(BookingReservationRepository bookingReservationRepository,
                              BookingDetailRepository bookingDetailRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _bookingDetailRepository = bookingDetailRepository;
        }

        public void BookRoom(int roomId, DateTime StartDate, DateTime EndDate, decimal actualPrice, int customerId)
        {
            int nextBookingReservationId = GetNextBookingReservationId();
            // Create booking reservation
            var bookingReservation = new BookingReservation
            {
                BookingReservationID = nextBookingReservationId,
                BookingDate = DateTime.Now,
                TotalPrice = actualPrice,
                CustomerId = customerId,
                BookingStatus = 1
            };

            _bookingReservationRepository.Add(bookingReservation);

           
            var bookingDetail = new BookingDetail
            {
                BookingReservationID = nextBookingReservationId,
                RoomID = roomId,
                StartDate = StartDate,
                EndDate = EndDate,
                ActualPrice = actualPrice
            };

            _bookingDetailRepository.Add(bookingDetail);
        }

        public void UpdateBooking(BookingReservationDTO reservationDTO, BookingDetailDTO detailDTO)
        {
            var reservationToUpdate = new BookingReservation
            {
                BookingReservationID = reservationDTO.BookingReservationID,
                BookingDate = reservationDTO.BookingDate,
                TotalPrice = reservationDTO.TotalPrice,
                CustomerId = reservationDTO.CustomerId,
                BookingStatus = reservationDTO.BookingStatus
            };

            _bookingReservationRepository.Update(reservationToUpdate);

            var detailToUpdate = new BookingDetail
            {
                BookingReservationID = detailDTO.BookingReservationID,
                RoomID = detailDTO.RoomID,
                StartDate = detailDTO.StartDate,
                EndDate = detailDTO.EndDate,
                ActualPrice = detailDTO.ActualPrice
            };

            _bookingDetailRepository.Update(detailToUpdate);
        }
        public bool CancelBooking(int bookingReservationId)
        {
            var bookingDetails = _bookingDetailRepository.GetAll();
            var detailToDelete = bookingDetails.Find(bd => bd.BookingReservationID == bookingReservationId);
            if (detailToDelete == null)
            {
                return false;
            }
            
            _bookingDetailRepository.Delete(detailToDelete.BookingReservationID);

            _bookingReservationRepository.Delete(bookingReservationId);
            return true;
        }

        public List<BookingReservationDTO> GetAllBookings()
        {
            var bookingReservations = _bookingReservationRepository.GetAll();
            var bookingReservationDTOs = MapToBookingReservationDTOs(bookingReservations);
            return bookingReservationDTOs;
        }

        public List<BookingReservationDTO> GetReservationsByCustomerId(int customerId)
        {
            var bookingReservations = _bookingReservationRepository.GetReservationsByCustomerId(customerId);
            var bookingDTOs = MapToBookingReservationDTOs(bookingReservations);
            return bookingDTOs;
        }

        public List<BookingDetailDTO> GetAllBookingDetails()
        {
            var bookingDetails = _bookingDetailRepository.GetAll();
            var bookingDetailDTOs = MapToBookingDetailDTOs(bookingDetails);
            return bookingDetailDTOs;
        }
        public BookingDetailDTO GetBookingDetail(int id)
        {
            var detail = _bookingDetailRepository.GetById(id);
            var dto = new BookingDetailDTO
            {
                BookingReservationID = detail.BookingReservationID,
                RoomID = detail.RoomID,
                StartDate = detail.StartDate,
                EndDate = detail.EndDate,
                ActualPrice = detail.ActualPrice
            };
            return dto;
        }
        public BookingReservationDTO GetBookingById(int id)
        {
            var reservation = _bookingReservationRepository.GetById(id);
            var dto = new BookingReservationDTO
            {
                BookingReservationID = reservation.BookingReservationID,
                BookingDate = reservation.BookingDate,
                TotalPrice = reservation.TotalPrice,
                CustomerId = reservation.CustomerId,
                BookingStatus = reservation.BookingStatus
            };
            return dto;
        }
        private List<BookingReservationDTO> MapToBookingReservationDTOs(List<BookingReservation> bookingReservations)
        {
            var bookingReservationDTOs = new List<BookingReservationDTO>();
            foreach (var reservation in bookingReservations)
            {
                var dto = new BookingReservationDTO
                {
                    BookingReservationID = reservation.BookingReservationID,
                    BookingDate = reservation.BookingDate,
                    TotalPrice = reservation.TotalPrice,
                    CustomerId = reservation.CustomerId,
                    BookingStatus = reservation.BookingStatus
                };
                bookingReservationDTOs.Add(dto);
            }
            return bookingReservationDTOs;
        }

        private List<BookingDetailDTO> MapToBookingDetailDTOs(List<BookingDetail> bookingDetails)
        {
            var bookingDetailDTOs = new List<BookingDetailDTO>();
            foreach (var detail in bookingDetails)
            {
                var dto = new BookingDetailDTO
                {
                    BookingReservationID = detail.BookingReservationID,
                    RoomID = detail.RoomID,
                    StartDate = detail.StartDate,
                    EndDate = detail.EndDate,
                    ActualPrice = detail.ActualPrice
                };
                bookingDetailDTOs.Add(dto);
            }
            return bookingDetailDTOs;
        }
        private int GetNextBookingReservationId()
        {
            var allReservations = _bookingReservationRepository.GetAll();
            if (allReservations.Count == 0)
            {
                return 1;
            }
            int maxId = allReservations.Max(r => r.BookingReservationID);
            return maxId + 1;
        }
    }
   

}
