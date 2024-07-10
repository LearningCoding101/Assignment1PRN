using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using FUMiniHotelSystem.service;
using FUMiniHotelSystem.repository;
using FUMiniHotelSystem.model;
using Microsoft.Identity.Client.NativeInterop;

namespace FUMiniHotelSystem.viewModel.admin.login {
    /// <summary>  
    /// Interaction logic for MainWindow.xaml  
    /// </summary>   
    public partial class LoginPage: Window {

        private readonly AuthenticationService _authenticationService;

        public delegate void LoginSuccessfulEventHandler(object sender, LoginEventArgs e);
        public event LoginSuccessfulEventHandler LoginSuccessful;

        public LoginPage() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];
            _authenticationService = new AuthenticationService(new AuthenticateRepository(connectionString));
        }

        RegisterPage registerPage = new RegisterPage();
        MainWindow mainWindow = new MainWindow();

        private void buttonLogin_Click(object sender, RoutedEventArgs e) {
            if (textBoxEmail.Text.Length == 0) {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            } else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")) {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            } else {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                var user = _authenticationService.AuthenticateUser(email, password);
                if (user != null) {
                    OnLoginSuccessful(new LoginEventArgs(user));
                    Close();
                } else {
                    errormessage.Text = "Please enter existing email/password !!";
                }
            }
        }

        protected virtual void OnLoginSuccessful(LoginEventArgs e) {
            LoginSuccessful?.Invoke(this, e);
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e) {
            registerPage.Show();
            Close();
        }
    }

    public class LoginEventArgs: EventArgs {
        public Customer Customer { get; }

        public LoginEventArgs(Customer customer) {
            Customer = customer;
        }
    }

}

