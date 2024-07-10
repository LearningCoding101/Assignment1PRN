using FUMiniHotelSystem.service;
using System.Windows;

namespace FUMiniHotelSystem.viewModel.customer {
    public partial class CustomerDashboard: Window {
        public CustomerDashboard(CustomerService customerService, int customerId) {
            InitializeComponent();
            DataContext = new CustomerDashboardViewModel(customerService, customerId);
        }
    }
}
