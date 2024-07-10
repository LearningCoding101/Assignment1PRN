using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System.Configuration;
using System.Windows.Controls;

namespace FUMiniHotelSystem.view
{
    /// <summary>
    /// Interaction logic for AccountSettings.xaml
    /// </summary>
    public partial class AccountSettings : Page
    {
        private readonly int _customerId;
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;
        private static string _connectionString = ConfigurationManager.AppSettings["MyDBConnectionString"]!;
        public AccountSettings()
        {
            InitializeComponent();
            _customerRepository = new CustomerRepository(_connectionString);
            _customerService = new CustomerService(_customerRepository);
            LoadData(_customerId);
        }

        public AccountSettings(int customerId)
        {
            InitializeComponent();
            _customerId = customerId;
            _customerRepository = new CustomerRepository(_connectionString);
            _customerService = new CustomerService(_customerRepository);
            LoadData(customerId);
        }

        private void LoadData(int customerId)
        {
            var customer = _customerService.GetCustomerById(customerId);
            CustomerFullname.Text = customer.CustomerFullName;
            Telephone.Text = customer.Telephone;
            EmailAddress.Text = customer.EmailAddress;
            CustomerBirthday.Text = customer.CustomerBirthday.ToString();
        }

        private void SaveChangesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var findCustimer = _customerService.GetCustomerById(_customerId);
            var customer = new CustomerDTO
            {
                CustomerId = _customerId,
                CustomerFullName = CustomerFullname.Text,
                Telephone = Telephone.Text,
                EmailAddress = EmailAddress.Text,
                CustomerBirthday = DateTime.Parse(CustomerBirthday.Text),
                CustomerStatus = findCustimer.CustomerStatus,
                Password = findCustimer.Password,
            };
            _customerService.UpdateCustomer(customer);
        }

        private void SettingButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadData(_customerId);
        }

        private void ViewReservationButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ViewReservation(_customerId));
        }
    }
}
