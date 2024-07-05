using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FUMiniHotelSystem.viewModel.admin.booking
{
    public partial class UpdateBooking : Window, INotifyPropertyChanged
    {
        private readonly BookingService _bookingService;
        private readonly CustomerService _customerService;
        private ObservableCollection<CustomerDTO> _customerList;

        private readonly int? _bookingReservationId;
        private readonly RoomService _roomService;

        private ObservableCollection<RoomDTO> _roomList;

        public ObservableCollection<CustomerDTO> CustomerList
        {
            get { return _customerList; }
            set
            {
                _customerList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RoomDTO> RoomList
        {
            get { return _roomList; }
            set
            {
                _roomList = value;
                OnPropertyChanged();
            }
        }
        public UpdateBooking(BookingService bookingService, RoomService roomService, CustomerService customerService, int? bookingReservationId = null)
        {
            InitializeComponent();
            DataContext = this; // Set the DataContext to this window
            _bookingService = bookingService;
            _bookingReservationId = bookingReservationId;
            _customerService = customerService;
            _roomService = roomService;
            LoadCustomers();
            LoadRooms();
            if (_bookingReservationId.HasValue)
            {
                // Load existing booking details for update
                LoadBookingDetails();
            }
            else
            {
                // Initialize for adding new booking
                InitializeForNewBooking();
            }
        }

        private void LoadCustomers()
        {
            CustomerList = new ObservableCollection<CustomerDTO>(_customerService.GetAllCustomers());
        }
        private void LoadRooms()
        {
            RoomList = new ObservableCollection<RoomDTO>(_roomService.GetAllRooms());
        }
        private void LoadBookingDetails()
        {
            var bookingReservation = _bookingService.GetBookingById(_bookingReservationId.Value);
            BookingIdTextBox.Text = bookingReservation.BookingReservationID.ToString();
            BookingDatePicker.SelectedDate = bookingReservation.BookingDate;
            TotalPriceTextBox.Text = bookingReservation.TotalPrice.ToString();

            // Select the current customer in ComboBox
            CustomerComboBox.SelectedValue = bookingReservation.CustomerId;

            var bookingDetail = _bookingService.GetBookingDetail(_bookingReservationId.Value);
            StartDatePicker.SelectedDate = bookingDetail.StartDate;
            EndDatePicker.SelectedDate = bookingDetail.EndDate;
            ActualPriceTextBox.Text = bookingDetail.ActualPrice.ToString();
        }

        private void InitializeForNewBooking()
        {
            // Initialize UI for adding new booking
            BookingIdTextBox.Text = "New Booking"; // Optionally set a placeholder or label
            BookingDatePicker.SelectedDate = DateTime.Today; // Set default date
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MinValue;
            decimal actualPrice = decimal.Parse(ActualPriceTextBox.Text);

            var updatedReservation = new BookingReservationDTO
            {
                BookingReservationID = _bookingReservationId ?? 0, // Use 0 for new booking
                BookingDate = BookingDatePicker.SelectedDate,
                TotalPrice = decimal.Parse(TotalPriceTextBox.Text),
                CustomerId = (int)CustomerComboBox.SelectedValue,
                BookingStatus = 1 // Set default booking status
            };

            var updatedDetail = new BookingDetailDTO
            {
                BookingReservationID = _bookingReservationId ?? 0, // Use 0 for new booking
                RoomID = (int)RoomComboBox.SelectedValue,
                StartDate = startDate,
                EndDate = endDate,
                ActualPrice = actualPrice
            };

            if (_bookingReservationId.HasValue)
            {
                // Update existing booking
                _bookingService.UpdateBooking(updatedReservation, updatedDetail);
                MessageBox.Show("Booking updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Add new booking
                _bookingService.BookRoom(updatedDetail.RoomID, startDate, endDate, actualPrice, updatedReservation.CustomerId);
                MessageBox.Show("Booking added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
