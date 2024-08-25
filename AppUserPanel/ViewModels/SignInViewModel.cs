using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppUserPanel.Commands;
using AppUserPanel.Pages;

namespace AppUserPanel.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private string _textName;
        private string _password;

        public string TextName
        {
            get => _textName;
            set
            {
                _textName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand { get; set; }

        public SignInViewModel()
        {
            SignInCommand = new RelayCommand(SignIn, CanSignIn);
        }

        private void SignIn(object? obj)
        {
            if (TextName == "user" && Password == "password" && obj is Page page)
            {
                page.NavigationService.Navigate(App.Container.GetInstance<Dashboard>());
                MessageBox.Show("Sign-In Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSignIn(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(TextName) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
