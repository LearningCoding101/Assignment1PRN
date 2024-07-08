using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FUMiniHotelSystem.viewModel.admin.customer {
    public partial class CustomerUpdateDialog: Window, INotifyPropertyChanged {
        private readonly CustomerService _customerService;
        private readonly int? _customerId;

        private CustomerDTO _customer;

        public CustomerDTO Customer {
            get { return _customer; }
            set {
                _customer = value;
                OnPropertyChanged();
            }
        }

        public CustomerUpdateDialog(CustomerService customerService, int? customerId = null) {
            InitializeComponent();
            DataContext = this; // Set the DataContext to this window
            _customerService = customerService;
            _customerId = customerId;

            if (_customerId.HasValue) {
                // Load existing customer details for update
                LoadCustomerDetails();
            } else {
                // Initialize for adding new customer
                InitializeForNewCustomer();
            }
        }

        private void LoadCustomerDetails() {
            Customer = _customerService.GetCustomerById(_customerId.Value);
            FullNameTextBox.Text = Customer.CustomerFullName;
            PhoneTextBox.Text = Customer.Telephone;
            EmailTextBox.Text = Customer.EmailAddress;
            BirthdayTextBox.Text = Customer.CustomerBirthday.ToString();
            StatusTextBox.Text = Customer.CustomerStatus.ToString();
        }

        private void InitializeForNewCustomer() {
            // Initialize UI for adding new customer
            Customer = new CustomerDTO();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(FullNameTextBox.Text) ||
                string.IsNullOrEmpty(PhoneTextBox.Text) ||
                string.IsNullOrEmpty(EmailTextBox.Text) ||
                string.IsNullOrEmpty(BirthdayTextBox.Text) ||
                string.IsNullOrEmpty(StatusTextBox.Text)) {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Customer.CustomerFullName = FullNameTextBox.Text;
            Customer.Telephone = PhoneTextBox.Text;
            Customer.EmailAddress = EmailTextBox.Text;
            var parsedDate = DateTime.Parse(BirthdayTextBox.Text);
            Customer.CustomerBirthday = parsedDate;
            var parsedByte = Byte.Parse(StatusTextBox.Text);
            Customer.CustomerStatus = parsedByte;

            if (_customerId.HasValue) {
                // Update existing customer
                _customerService.UpdateCustomer(Customer);
                MessageBox.Show("Customer updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
                // Add new customer
                _customerService.AddCustomer(Customer);
                MessageBox.Show("Customer added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
