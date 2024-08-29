using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using AppUserPanel.Pages;

namespace AppUserPanel.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private string _textName;
        private string _password;
        MirtalibDbContext dbContext;
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
            dbContext = new();
            SignInCommand = new RelayCommand(SignIn, CanSignIn);
            BackCommand = new RelayCommand(BackCommandExecute);
        }

        private void SignIn(object? obj)
        {
            
                var user = dbContext.Users.FirstOrDefault(x => x.UserName == TextName && x.Password == PasswordHasher.HashPassword(Password));
                if (user != null && obj is Page page)
                {
                    PasswordHasher.UserId = user.Id;

                    page.NavigationService.Navigate(new Dashboard());
                    MessageBox.Show("Sign-In Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    TextName = string.Empty;
                    Password = string.Empty;
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
