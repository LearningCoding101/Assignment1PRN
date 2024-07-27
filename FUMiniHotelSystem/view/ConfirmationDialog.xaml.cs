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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FUMiniHotelSystem.view
{
    /// <summary>
    /// Interaction logic for ConfirmationDialog.xaml
    /// </summary>
    public partial class ConfirmationDialog : Page
    {
        private readonly BookingService _service;
        private readonly BookingDetailRepository _bookingDetailRepository;
        private readonly BookingReservationRepository _bookingReservationRepository;
        private static string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"]!;
        private readonly int _customerId;
        private readonly int _bookingId;
        public ConfirmationDialog(int customerId, int bookingId)
        {
            InitializeComponent();
            _customerId = customerId;
            _bookingId = bookingId;
            _bookingDetailRepository = new BookingDetailRepository(connectionString);
            _bookingReservationRepository = new BookingReservationRepository(connectionString);
            _service = new BookingService(_bookingReservationRepository, _bookingDetailRepository);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool success = _service.CancelBooking(_bookingId);

            if (success)
            {
                NavigationService?.Navigate(new ViewReservation(_customerId));
            }
            else
            {
                MessageBox.Show("Failed to cancel booking.");
            }
        }

        private void RejectConfirmation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ViewReservation(_customerId));
        }
    }
}
