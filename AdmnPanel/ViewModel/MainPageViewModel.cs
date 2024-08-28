using AppAdminPanel.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using AppAdminPanel.Pages;
namespace AppAdminPanel.ViewModel;

public class MainPageViewModel : BaseViewModel
{

    #region Commands
 
    public ICommand SignInCommand { get; set; }
    public ICommand CloseCommand { get; set; }
    #endregion

    #region Ctor

    public MainPageViewModel()
    { 
        SignInCommand = new RelayCommand(SignInCommandExecute);
       
         
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
      
            page.NavigationService.Navigate(new SignInPage());
        }
    }

     
    #endregion


}
