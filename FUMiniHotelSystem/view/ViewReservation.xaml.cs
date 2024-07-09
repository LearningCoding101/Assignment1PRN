﻿using FUMiniHotelSystem.model;
using FUMiniHotelSystem.service;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotelSystem.view
{
    /// <summary>
    /// Interaction logic for ViewReservation.xaml
    /// </summary>
    public partial class ViewReservation : Page
    {
        private readonly BookingService _service;
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly BookingReservationRepository _bookingReservationRepository;
        private static string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"]!;
        private readonly int _customerId;

        public ViewReservation()
        {
            InitializeComponent();

            _bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingReservationRepository = new BookingReservationRepository(connectionString);
            _service = new BookingService(_bookingReservationRepository, _bookingDetailRepository);
            LoadData();
        }

        public ViewReservation(int customerId)
        {
            InitializeComponent();

            _bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingReservationRepository = new BookingReservationRepository(connectionString);
            _service = new BookingService(_bookingReservationRepository, _bookingDetailRepository);
            _customerId = customerId;
            LoadData(customerId);
        }

        private void LoadData()
        {
            var bookings = _service.GetAllBookings();
            ReservationList.ItemsSource = bookings;
        }

        private void LoadData(int customerId)
        {
            var bookings = _service.GetReservationsByCustomerId(customerId);
            ReservationList.ItemsSource = bookings;
        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is BookingReservation booking)
            {
                var bookingId = booking.BookingReservationID;

                bool success = _service.CancelBooking(bookingId);
                
                if(success)
                {
                    LoadData(_customerId);
                }
                else
                {
                    MessageBox.Show("Failed to cancel booking.");
                }
            }
        }

        private void ViewDetailButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewReservationButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AccountSettings(_customerId));
        }

        private void SearchReservations(object sender, RoutedEventArgs e)
        {
            try
            {
                if(int.TryParse(SearchReservation.Text, out int customerId))
                {
                    LoadData(customerId);
                }
                else
                {
                    LoadData(_customerId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
