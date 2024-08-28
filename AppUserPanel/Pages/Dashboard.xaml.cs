using AppUserPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppUserPanel.Pages;
 
public partial class Dashboard : Page
{
    int id;
    public Dashboard()
    {
        InitializeComponent();
        DataContext = new DashboardViewModel();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure Close?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        if (result == MessageBoxResult.OK)
        {
            Window.GetWindow(this)?.Close();

        }


    }

}
