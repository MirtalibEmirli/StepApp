using AppUserPanel.Commands;
using AppUserPanel.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppUserPanel.ViewModels
{
    internal class MainViewModel
    {
        #region Commands

        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Ctor

        public MainViewModel()
        {
            SignInCommand = new RelayCommand(SignInCommandExecute);
            SignUpCommand = new RelayCommand(SignUpCommandExecute);
            CloseCommand = new RelayCommand(CloseCommandExecute);
        }

        #endregion

        #region Functions

        private void CloseCommandExecute(object? obj)
        {
            if (obj is Page page)
            {
                Window window = Window.GetWindow(page);
                window?.Close();
            }
        }

        private void SignInCommandExecute(object obj)
        {
            if (obj is Page page)
            { 
                page.NavigationService?.Navigate(new SignInPage());
            }
        }

        private void SignUpCommandExecute(object obj)
        {
            if (obj is Page page)
            {
                page.NavigationService?.Navigate(new SignUpPage());
            }
        }

        #endregion
    }
}
