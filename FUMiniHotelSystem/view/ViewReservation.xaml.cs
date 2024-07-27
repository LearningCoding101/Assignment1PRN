using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using FUMiniHotelSystem.service;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotelSystem.view {
    /// <summary>
    /// Interaction logic for ViewReservation.xaml
    /// </summary>
    public partial class ViewReservation: Page {
        private readonly BookingService _service;
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly BookingReservationRepository _bookingReservationRepository;
        private static string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];
        private readonly int _customerId;

        public ViewReservation() {
            InitializeComponent();

            _bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingReservationRepository = new BookingReservationRepository(connectionString);
            _service = new BookingService(_bookingReservationRepository, _bookingDetailRepository);
            LoadData();
        }

        public ViewReservation(int customerId) {
            InitializeComponent();

            _bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingReservationRepository = new BookingReservationRepository(connectionString);
            _service = new BookingService(_bookingReservationRepository, _bookingDetailRepository);
            _customerId = customerId;
            LoadData(customerId);
        }

        private void LoadData() {
            var bookings = _service.GetAllBookings();
            ReservationDataGrid.ItemsSource = bookings;
        }

        private void LoadData(int customerId) {
            var bookings = _service.GetReservationsByCustomerId(customerId);
            ReservationDataGrid.ItemsSource = bookings;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            if (ReservationDataGrid.SelectedItem != null) {
                var reservation = (BookingReservationDTO) ReservationDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) {
                    _service.CancelBooking(reservation.BookingReservationID);
                    LoadData();
                }
            } else {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e) {
            if (ReservationDataGrid.SelectedItem != null) {
                var reservation = (BookingReservationDTO) ReservationDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) {
                    _service.CancelBooking(reservation.BookingReservationID);
                    LoadData();
                }
            } else {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void ViewReservationButton_Click(object sender, RoutedEventArgs e) {
            LoadData(_customerId);
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e) {
            NavigationService?.Navigate(new AccountSettings(_customerId));
        }

        private void SearchReservations(object sender, RoutedEventArgs e) {
            try {
                if (int.TryParse(SearchReservation.Text, out int customerId)) {
                    LoadData(customerId);
                } else {
                    LoadData(_customerId);
                }
            } catch (Exception) {
                throw;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var newReservationWindow = new NewReservation(_customerId);
            newReservationWindow.ShowDialog();

            LoadData(_customerId);
        }
    }
}
