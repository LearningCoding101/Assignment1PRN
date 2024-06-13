using FUMiniHotelSystem.model;
using FUMiniHotelSystem.repository;
using FUMiniHotelSystem.service;
using System.ComponentModel;
using System.Windows.Input;

namespace FUMiniHotelSystem.viewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthenticationService _service;

        private string _emailAddress = null!;
        private string _password = null!;
        private string _customerFullName = null!;
        private string _telephone = null!;
        private DateTime _customerBirthday;
        private string _role = null!;
        private bool _isAuthenticated;
        private Customer? _authenticatedUser;
        private string? _searchKeyword;
        private IEnumerable<Customer>? _searchResults;

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                if (_emailAddress != value)
                {
                    _emailAddress = value;
                    OnPropertyChanged(nameof(EmailAddress));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string CustomerFullName
        {
            get => _customerFullName;
            set
            {
                if (_customerFullName != value)
                {
                    _customerFullName = value;
                    OnPropertyChanged(nameof(CustomerFullName));
                }
            }
        }

        public string Telephone
        {
            get => _telephone;
            set
            {
                if (_telephone != value)
                {
                    _telephone = value;
                    OnPropertyChanged(nameof(Telephone));
                }
            }
        }

        public DateTime CustomerBirthday
        {
            get => _customerBirthday;
            set
            {
                if (_customerBirthday != value)
                {
                    _customerBirthday = value;
                    OnPropertyChanged(nameof(CustomerBirthday));
                }
            }
        }

        public string Role
        {
            get => _role;
            private set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged(nameof(Role));
                }
            }
        }

        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            private set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged(nameof(IsAuthenticated));
                }
            }
        }

        public Customer AuthenticatedUser
        {
            get => _authenticatedUser;
            private set
            {
                if (_authenticatedUser != value)
                {
                    _authenticatedUser = value;
                    OnPropertyChanged(nameof(AuthenticatedUser));
                }
            }
        }

        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (_searchKeyword != value)
                {
                    _searchKeyword = value;
                    OnPropertyChanged(nameof(SearchKeyword));
                }
            }
        }

        public IEnumerable<Customer> SearchResults
        {
            get => _searchResults;
            private set
            {
                if (_searchResults != value)
                {
                    _searchResults = value;
                    OnPropertyChanged(nameof(SearchResults));
                }
            }
        }

        public LoginViewModel()
        {
            var authRepo = new AuthenticateRepository("Data Source=Zincerious;Initial Catalog=FUMiniHotelSystem;User ID=sa;Password=12345;Encrypt=False;Trust Server Certificate=True");
            _service = new AuthenticationService(authRepo);
        }

        public ICommand LoginCommand => new RelayCommand(Login);
        public ICommand SearchCommand => new RelayCommand(Search);
        public ICommand RegisterCommand => new RelayCommand(Register);

        private void Login()
        {
            AuthenticatedUser = _service.AuthenticateUser(EmailAddress, Password);
            IsAuthenticated = AuthenticatedUser != null;
            if(IsAuthenticated)
            {
                Role = _service.GetUserRole(EmailAddress);
            }
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(AuthenticatedUser));
        }

        private void Search()
        {
            SearchResults = _service.SearchCustomer(SearchKeyword);
            OnPropertyChanged(nameof(SearchResults));
        }

        private void Register()
        {
            var newCustomer = new Customer
            {
                CustomerFullName = CustomerFullName,
                Telephone = Telephone,
                EmailAddress = EmailAddress,
                CustomerBirthday = CustomerBirthday,
                CustomerStatus = true,
                Password = Password,
            };

            _service.RegisterUser(newCustomer);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
