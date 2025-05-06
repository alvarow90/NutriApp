using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NutriFitApp.Shared.DTOs;
using NutriFitApp.Mobile.Services;
using System.Threading.Tasks;
using System;
using Microsoft.Maui.Controls;

namespace NutriFitApp.Mobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        public event PropertyChangedEventHandler? PropertyChanged;


        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        private bool _hasError;
        public bool HasError
        {
            get => _hasError;
            set { _hasError = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthService(new HttpClient());
            LoginCommand = new Command(async () => await LoginAsync());
        }

        private async Task LoginAsync()
        {
            HasError = false;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email y contraseña son requeridos.";
                HasError = true;
                return;
            }

            try
            {
                var loginDto = new LoginDTO { Email = Email, Password = Password };
                bool result = await _authService.LoginAsync(loginDto);

                if (result)
                {
                    await Shell.Current.GoToAsync("//Dashboard");
                }
                else
                {
                    ErrorMessage = "Credenciales inválidas.";
                    HasError = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error: {ex.Message}";
                HasError = true;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
