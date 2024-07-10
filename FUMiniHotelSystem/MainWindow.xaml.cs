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
        }

        private void NavigateTest(object sender, RoutedEventArgs e) {
            MainFrame.NavigationService.Navigate(new dashboard());

        }

        private void NavigateReservationList(object sender, RoutedEventArgs e) {
            MainFrame.NavigationService.Navigate(new ViewReservation());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            this.Hide(); // Hide the main window
            LoginPage loginPopUp = new LoginPage();
            loginPopUp.LoginSuccessful += LoginPopUp_LoginSuccessful;
            loginPopUp.ShowDialog();
            this.Show();
        }
        private void LoginPopUp_LoginSuccessful(object sender, LoginEventArgs e) {
            LoggedInUser = e.Customer.CustomerFullName;
            MainFrame.NavigationService.Navigate(new ViewReservation(e.Customer.CustomerId));
        }
    }
}