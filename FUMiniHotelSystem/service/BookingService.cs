using FUMiniHotelSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUMiniHotelSystem.service
{
    internal class BookingService
    {
        private readonly BookingReservationRepository _bookingReservationRepository;
        private readonly BookingDetailRepository _bookingDetailRepository;
        public BookingService(BookingReservationRepository bookingReservationRepository,
                           BookingDetailRepository bookingDetailRepository)
        {
            _bookingReservationRepository = bookingReservationRepository;
            _bookingDetailRepository = bookingDetailRepository;
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
