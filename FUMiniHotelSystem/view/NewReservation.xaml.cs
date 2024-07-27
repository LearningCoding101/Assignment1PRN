using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace FUMiniHotelSystem.view
{
    /// <summary>
    /// Interaction logic for NewReservation.xaml
    /// </summary>
    public partial class NewReservation : Window
    {
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly int _customerId;
        private static string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"]!;

        public NewReservation(int customerId)
        {
            InitializeComponent();

            var bookingReservationRepository = new BookingReservationRepository(connectionString);
            var bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingService = new BookingService(bookingReservationRepository, bookingDetailRepository);

            var roomRepository = new RoomRepository(connectionString);
            var roomTypeRepository = new RoomTypeRepository(connectionString);
            _roomService = new RoomService(roomRepository, roomTypeRepository);

            _customerId = customerId;

            LoadAvailableRooms();
            SetupDatePickers();
        }

        private void LoadAvailableRooms()
        {
            List<RoomDTO> rooms = _roomService.GetAllRooms();
            RoomComboBox.ItemsSource = rooms;
            RoomComboBox.DisplayMemberPath = "RoomDetailDescription";
            RoomComboBox.SelectedValuePath = "RoomID";
        }

        private void SetupDatePickers()
        {
            StartDatePicker.SelectedDate = DateTime.Today;
            EndDatePicker.SelectedDate = DateTime.Today.AddDays(1);

            StartDatePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
            EndDatePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
            RoomComboBox.SelectionChanged += RoomComboBox_SelectionChanged;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ValidateDates())
            {
                UpdatePrice();
            }
        }

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            if (RoomComboBox.SelectedItem is RoomDTO selectedRoom &&
                StartDatePicker.SelectedDate.HasValue &&
                EndDatePicker.SelectedDate.HasValue &&
                ValidateDates())
            {
                int days = (EndDatePicker.SelectedDate.Value - StartDatePicker.SelectedDate.Value).Days;
                decimal totalPrice = (decimal)(selectedRoom.RoomPricePerDay * days);
                PriceTextBlock.Text = $"${totalPrice:F2}";
            }
            else
            {
                PriceTextBlock.Text = "";
            }
        }

        private bool ValidateDates()
        {
            if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                DateTime startDate = StartDatePicker.SelectedDate.Value;
                DateTime endDate = EndDatePicker.SelectedDate.Value;

                if (startDate > endDate)
                {
                    MessageBox.Show("Start date cannot be after end date.", "Date Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        }

        private void ConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            if (RoomComboBox.SelectedItem is RoomDTO selectedRoom &&
                StartDatePicker.SelectedDate.HasValue &&
                EndDatePicker.SelectedDate.HasValue &&
                ValidateDates())
            {
                DateTime startDate = StartDatePicker.SelectedDate.Value;
                DateTime endDate = EndDatePicker.SelectedDate.Value;
                int roomId = selectedRoom.RoomID;
                decimal actualPrice = decimal.Parse(PriceTextBlock.Text.Trim('$'));

                try
                {
                    _bookingService.BookRoom(roomId, startDate, endDate, actualPrice, _customerId);
                    MessageBox.Show("Booking successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error occurred while booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a room and valid dates.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
