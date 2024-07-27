using FUMiniHotelSystem.view;
using FUMiniHotelSystem.viewModel.admin;
using FUMiniHotelSystem.viewModel.admin.room;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FUMiniHotelSystem.viewModel.admin.login;
using FUMiniHotelSystem.service;
using FUMiniHotelSystem.model;

namespace FUMiniHotelSystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        public string LoggedInUser { get; set; }
        public MainWindow() {
            InitializeComponent();
            UpdateLoginLogoutButton();
        }

        private void NavigateTest(object sender, RoutedEventArgs e) {
            MainFrame.NavigationService.Navigate(new Dashboard());

        }

        private void NavigateReservationList(object sender, RoutedEventArgs e) {
            MainFrame.NavigationService.Navigate(new ViewReservation());
        }

        private void LoginLogoutButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            if (LoggedInUser == null) {
                // Show login window
                LoginPage loginPopUp = new LoginPage();
                loginPopUp.LoginSuccessful += LoginPopUp_LoginSuccessful;
                loginPopUp.ShowDialog();
                UpdateLoginLogoutButton();
            } else {
                // Logout logic
                LoggedInUser = null;
                UpdateLoginLogoutButton();
                MainFrame.Content = null; // Clear the frame content or navigate to a default page
            }
            this.Show();
        }

        private void LoginPopUp_LoginSuccessful(object sender, LoginEventArgs e) {
            LoggedInUser = e.Customer.CustomerFullName;
            String Role = e.Role;
            if (Role == "Admin")
            {
                Dashboard adminWindow = new Dashboard();
                MainFrame.NavigationService.Navigate(adminWindow);
            }
            else
            {
                ViewReservation customerWindow = new ViewReservation(e.Customer.CustomerId);
                MainFrame.NavigationService.Navigate(customerWindow);

            }

            
        }
        private void UpdateLoginLogoutButton() {
            if (LoggedInUser == null) {
                LoginLogoutButton.Content = "Login";
            } else {
                LoginLogoutButton.Content = "Logout";
            }
        }
    }
}