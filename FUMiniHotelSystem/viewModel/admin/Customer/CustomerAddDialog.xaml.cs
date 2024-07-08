using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FUMiniHotelSystem.viewModel.admin.Customer {
    public partial class CustomerAddDialog: Window, INotifyPropertyChanged {
        private readonly CustomerService _customerService;

        private CustomerDTO _customer;

        public CustomerDTO Customer {
            get { return _customer; }
            set {
                _customer = value;
                OnPropertyChanged();
            }
        }

        public CustomerAddDialog(CustomerService customerService) {
            InitializeComponent();
            DataContext = this; // Set the DataContext to this window
            _customerService = customerService;
            InitializeForNewCustomer();
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

            // Add new customer
            _customerService.AddCustomer(Customer);
            MessageBox.Show("Customer added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

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
