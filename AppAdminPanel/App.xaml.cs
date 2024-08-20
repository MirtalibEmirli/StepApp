using AppAdminPanel.Pages;
using AppAdminPanel.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
namespace AppAdminPanel
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
        }

        public void RegisterViewModels()
        {
            Container.RegisterSingleton<MainPageViewModel>();
            Container.RegisterSingleton<SignInPageViewModel>();
        }
    }
}
