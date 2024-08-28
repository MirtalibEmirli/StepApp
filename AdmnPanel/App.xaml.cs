using AdmnPanel.Pages;
using AdmnPanel.ViewModel;
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
           /* Container.RegisterSingleton<MainPage>();
            Container.RegisterSingleton<SignInPage>();
            Container.RegisterSingleton<Dashboard>();
            Container.RegisterSingleton<AddCategoryPage>();
            Container.RegisterSingleton<CategoryPage>();
            Container.Register<AddProductPage>();
            Container.Register<UserPage>();
           */
        }

        public void RegisterViewModels()
        {
            /*
            Container.RegisterSingleton<MainPageViewModel>();
            Container.RegisterSingleton<SignInPageViewModel>();
            Container.RegisterSingleton<DashboardViewModel>();
            Container.RegisterSingleton<AddCategoryPageViewModel>();
            Container.RegisterSingleton<CategoryViewModel>();
            Container.RegisterSingleton<AddProductViewModel>();
            Container.RegisterSingleton<UserViewModel>();*/
        }
    }
}
