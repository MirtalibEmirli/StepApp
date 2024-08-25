using AppUserPanel.Viewmodels;
using AppUserPanel.ViewModels;
using System.Windows.Controls;
 
namespace AppUserPanel.Pages
{
    
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            DataContext = App.Container.GetInstance<SignInViewModel>();
        }
    }
}
