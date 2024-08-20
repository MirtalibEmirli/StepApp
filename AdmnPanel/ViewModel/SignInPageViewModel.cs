using AppAdminPanel.Commands;
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
        MessageBox.Show("Ela");
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
