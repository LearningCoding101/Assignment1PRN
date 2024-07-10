using FUMiniHotelSystem.dto;
using FUMiniHotelSystem.repository;
using FUMiniHotelSystem.service;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;

namespace FUMiniHotelSystem.viewModel.admin.login
{
    public partial class LoginPage : Window
    {
        private readonly AuthenticationService _authenticationService;
        public delegate void LoginSuccessfulEventHandler(object sender, LoginEventArgs e);
        public event LoginSuccessfulEventHandler LoginSuccessful;

        public LoginPage()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            string connectionString = ConfigurationManager.AppSettings["MyDbConnectionString"];
            _authenticationService = new AuthenticationService(new AuthenticateRepository(connectionString));
        }

        RegisterPage registerPage = new RegisterPage();
        MainWindow mainWindow = new MainWindow();

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                var loginRequest = new LoginRequestDTO
                {
                    EmailAddress = textBoxEmail.Text,
                    Password = passwordBox1.Password
                };

                var userDTO = _authenticationService.AuthenticateUser(loginRequest);
                if (userDTO != null)
                {
                    string role = _authenticationService.GetUserRole(userDTO.EmailAddress);
                    OnLoginSuccessful(new LoginEventArgs(userDTO, role));
                    Close();
                }
                else
                {
                    errormessage.Text = "Please enter existing email/password !!";
                }
            }
        }

        protected virtual void OnLoginSuccessful(LoginEventArgs e)
        {
            LoginSuccessful?.Invoke(this, e);
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registerPage.Show();
            Close();
        }
    }

    public class LoginEventArgs : EventArgs
    {
        public CustomerDTO Customer { get; }
        public string Role { get; }

        public LoginEventArgs(CustomerDTO customer, string role)
        {
            Customer = customer;
            Role = role;
        }
    }
}