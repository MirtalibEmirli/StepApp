using AdmnPanel.Pages;
using AppAdminPanel;
using AppAdminPanel.Commands;
using AppAdminPanel.Services;
using AppAdminPanel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AdmnPanel.ViewModel
{
   public class DashBoardViewModel:InotifyService
    {
        public ICommand CategoryCommand { get; set; }

        public DashBoardViewModel()
        {
            CategoryCommand = new RelayCommand(CategoryCommandExecute);
        }

        private void CategoryCommandExecute(object? obj)
        {
            try
            {
                if (obj is Page page && page.NavigationService != null)
                {
                    page.NavigationService.Navigate(App.Container.GetInstance<CategoryPage>());
                }
                else
                {
                    MessageBox.Show("NavigationService is null.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
