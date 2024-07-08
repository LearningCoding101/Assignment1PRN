using FUMiniHotelSystem.service;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotelSystem.viewModel.admin.Customer {
    public partial class ManageCustomer: Window {
        private readonly CustomerService _customerService;

        public ManageCustomer() {
            InitializeComponent();

            string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];
            _customerService = new CustomerService(new CustomerRepository(connectionString));
            LoadCustomers();
        }

        private void LoadCustomers() {
            var customers = _customerService.GetAllCustomers();
            CustomerDataGrid.ItemsSource = customers;
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e) {
            var addCustomerDialog = new UpdateCustomer(_customerService);
            addCustomerDialog.ShowDialog();
            LoadCustomers();
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e) {
            var customerId = ((Customer) CustomersDataGrid.SelectedItem).Id;
            var editCustomerDialog = new UpdateCustomer(_customerService, customerId);
            editCustomerDialog.ShowDialog();
            LoadCustomers();
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e) {
            var customerId = ((Customer) CustomersDataGrid.SelectedItem).Id;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Customer?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) {
                _customerService.DeleteCustomer(customerId);
                LoadCustomers();
            }
        }
    }
}
