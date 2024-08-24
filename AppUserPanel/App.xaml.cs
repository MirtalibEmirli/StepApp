using AppUserPanel.Pages;
using AppUserPanel.Viewmodels;
using AppUserPanel.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AppUserPanel
{
    
    public partial class App : Application
    {

        public static SimpleInjector.Container Container { get; set; } = new SimpleInjector.Container();

        public App()
        {
            RegisterViews();
            RegisterViewModels();
        }

        public void RegisterViews()
        {
            Container.RegisterSingleton<MainPage>();
            Container.RegisterSingleton<SignInPage>();
            Container.RegisterSingleton<SignUpPage>();
           
        }

        public void RegisterViewModels()
        {
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<SignInViewModel>();
            Container.RegisterSingleton<SignupViewModel>();
 
        }
    }

}
