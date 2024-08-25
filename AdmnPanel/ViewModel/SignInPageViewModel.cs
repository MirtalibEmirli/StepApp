using AdmnPanel.Pages;
using AppAdminPanel.Commands;
using AppAdminPanel.Pages;
using AppLibrary.Data;
using AppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AppAdminPanel.ViewModel;

public class SignInPageViewModel : BaseViewModel
{
    public string firstName { get; set; }
    public string FirstName
    {
        get { return firstName; }
        set { firstName = value;OnPropertyChanged(); }
    }
    public string password { get; set; }
    public string Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); }
    }
    MirtalibDbContext context = new();

    public ICommand CloseCommand { get; set; }
    public ICommand Entercommand { get; set; }
 
    public SignInPageViewModel()
    {
        CloseCommand = new RelayCommand(CloseCommandExecute);
        BackCommand = new RelayCommand(BackCommandExecute);
        Entercommand = new RelayCommand(EnterExecute, IsEnter);
       
    }

    private bool IsEnter(object? obj)
    {
        if (string.IsNullOrEmpty(Password))
            return false;

        try
        {
            var hashedPassword = PasswordHasher.HashPassword(Password);
            var admin = context.Admins.Single<Admin>();

            if (hashedPassword.SequenceEqual(admin.Password))
                return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return false;
    }


    private void EnterExecute(object? obj)
    {
        if (obj is Page page)
        {

            page.NavigationService.Navigate(App.Container.GetInstance<Dashboard>());
            FirstName = string.Empty;
            Password = string.Empty;
        }
    }

    private void CloseCommandExecute(object? obj)
    {
        if (obj is Page page)
        {
            Window window = Window.GetWindow(page);

            window?.Close();
        }
    }

}
