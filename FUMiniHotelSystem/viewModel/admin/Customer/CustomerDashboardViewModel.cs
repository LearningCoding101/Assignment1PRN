using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.service;
using FUMiniHotelSystem.viewModel.admin.customer;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FUMiniHotelSystem.viewModel.customer {
    public class CustomerDashboardViewModel: INotifyPropertyChanged {
        private readonly CustomerService _customerService;

        public ICommand ViewProfileCommand { get; }

        private CustomerDTO _currentCustomer;
        public CustomerDTO CurrentCustomer {
            get { return _currentCustomer; }
            set {
                _currentCustomer = value;
                OnPropertyChanged();
            }
        }

        public CustomerDashboardViewModel(CustomerService customerService, int customerId) {
            _customerService = customerService;
            CurrentCustomer = _customerService.GetCustomerById(customerId);
            ViewProfileCommand = new RelayCommand(OpenCustomerProfile);
        }

        private void OpenCustomerProfile() {
            var updateCustomerDialog = new UpdateCustomer(_customerService, CurrentCustomer.CustomerId);
            updateCustomerDialog.ShowDialog();
            // Reload customer data after potential update
            CurrentCustomer = _customerService.GetCustomerById(CurrentCustomer.CustomerId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
