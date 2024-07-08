using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.model;
using FUMiniHotelSystem.service;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FUMiniHotelSystem.viewModel.admin.customer
{
    public partial class ManageCustomer : Page
    {
        private readonly CustomerService _customerService;
        public List<CustomerDTO> Customers { get; set; }

        public ManageCustomer()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];
            _customerService = new CustomerService(new CustomerRepository(connectionString));

            // Initialize Customers collection
            Customers = new List<CustomerDTO>();
            LoadCustomers();

            // Set the DataContext of the page to itself
            DataContext = this;
        }

        private void LoadCustomers()
        {
            // Fetch customers from service
            Customers = _customerService.GetAllCustomers();
            // Set the ItemsSource of the DataGrid
            CustomerDataGrid.ItemsSource = Customers;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerDialog = new UpdateCustomer(_customerService);
            addCustomerDialog.ShowDialog();
            LoadCustomers();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem != null)
            {
                var customer = (CustomerDTO)CustomerDataGrid.SelectedItem;
                var editCustomerDialog = new UpdateCustomer(_customerService, customer.CustomerId);
                editCustomerDialog.ShowDialog();
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem != null)
            {
                var customer = (CustomerDTO)CustomerDataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Customer?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _customerService.DeleteCustomer(customer.CustomerId);
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }
    }
}
