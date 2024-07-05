using FUMiniHotelSystem.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FUMiniHotelSystem.viewModel.admin.booking
{
    /// <summary>
    /// Interaction logic for ViewBookingDetail.xaml
    /// </summary>
    public partial class ViewBookingDetail : Window
    {
        private readonly BookingService _bookingService;
        private readonly int _bookingReservationId;
        
        public ViewBookingDetail(BookingService bookingService, int bookingReservationId)
        {
            InitializeComponent();
            _bookingService = bookingService;
            _bookingReservationId = bookingReservationId;

            // Load existing booking details
            LoadData();
        }
        private void LoadData()
        {
            var bookingReservation = _bookingService.GetBookingById(_bookingReservationId);
            BookingIdTextBox.Text = bookingReservation.BookingReservationID.ToString();
            BookingDatePicker.SelectedDate = bookingReservation.BookingDate;
            TotalPriceTextBox.Text = bookingReservation.TotalPrice.ToString();
            CustomerIdTextBox.Text = bookingReservation.CustomerId.ToString();

            var bookingDetail = _bookingService.GetBookingDetail(_bookingReservationId);
            StartDatePicker.SelectedDate = bookingDetail.StartDate;
            EndDatePicker.SelectedDate = bookingDetail.EndDate;
            ActualPriceTextBox.Text = bookingDetail.ActualPrice.ToString();
        }
    }
}
