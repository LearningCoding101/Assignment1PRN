using FUMiniHotelSystem.model;
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

        public ViewReservation()
        {
            InitializeComponent();

            _bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingReservationRepository = new BookingReservationRepository(connectionString);
            _service = new BookingService(_bookingReservationRepository, _bookingDetailRepository);
            LoadData();
        }

        private void LoadData()
        {
            var bookings = _service.GetAllBookings();
            ReservationList.ItemsSource = bookings;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is BookingReservation booking)
            {
                var bookingId = booking.BookingReservationID;

                bool success = _service.CancelBooking(bookingId);
                
                if(success)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Failed to cancel booking.");
                }
            }
        }
    }
}
