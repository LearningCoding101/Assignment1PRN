using FUMiniHotelSystem.service;
using FUMiniHotelSystem.viewModel.admin.room;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotelSystem.viewModel.admin.booking
{
    public partial class ManageBooking : Page
    {
        private readonly BookingService _bookingService;
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;

        public ManageBooking()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];
            _bookingService = new BookingService(new BookingReservationRepository(connectionString), new BookingDetailRepository(connectionString));
            _customerService = new CustomerService(new CustomerRepository(connectionString));
            _roomService = new RoomService(new RoomRepository(connectionString), new RoomTypeRepository(connectionString));
            LoadBookings();
        }

        private void LoadBookings()
        {
            var bookings = _bookingService.GetAllBookings();
            BookingsDataGrid.ItemsSource = bookings; 
        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            int bookingId = (int)((Button)sender).Tag;
            var updateBookingDialog = new UpdateBooking(_bookingService, _roomService,_customerService, bookingId);
            updateBookingDialog.ShowDialog();
            LoadBookings();
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).Tag;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Booking?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _bookingService.CancelBooking(Id);
                LoadBookings();
            }
        }

        private void ViewDetail_Click(object sender, RoutedEventArgs e)
        {
            int bookingId = (int)((Button)sender).Tag;
            var booking = new ViewBookingDetail(_bookingService, bookingId);
            booking.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var addBookingDialog = new UpdateBooking(_bookingService,_roomService, _customerService);
            addBookingDialog.ShowDialog();
            LoadBookings();

        }
    }
}
