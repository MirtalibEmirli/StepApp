using AdmnPanel.Pages;
using AppAdminPanel;
using AppAdminPanel.Commands;
using AppAdminPanel.ViewModel;
using AppLibrary.Data;
using AppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdmnPanel.ViewModel
{
    public class CategoryViewModel:BaseViewModel
    {


        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        #endregion

        private ObservableCollection<Category> categoires { get; set; }
        public ObservableCollection<Category> Categories
        {
            get { return categoires; }
            set { categoires = value; OnPropertyChanged(); }

        }
        MirtalibDbContext dbContext;
        public CategoryViewModel()
        {
            LoadCommand = new RelayCommand(Load);
            dbContext = new MirtalibDbContext();
            AddCommand = new RelayCommand(AddExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            BackCommand = new RelayCommand(BackCommandExecute);
            Categories = new ObservableCollection<Category>(dbContext.Categories.ToList());
        }

        private void Load(object? obj)
        {
            Categories = new ObservableCollection<Category>(dbContext.Categories.ToList());
            MessageBox.Show("Categoires Refreshed");
        }

        private void DeleteExecute(object? obj)
        {
            try
            {
                if (obj is ListView categoryview)
                {
                    foreach(var item in Categories)
         {
                        if (item == categoryview.SelectedItem)
                        {
                            var a = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (a == MessageBoxResult.Yes)
                            {
                                Categories.Remove(item);
                                dbContext.Categories.Remove(item);
                                dbContext.SaveChanges();
                                return;
                            }

                            else
                            {
                                MessageBox.Show("Please select a correct category");
                            }

                        }

                    }
                }
                else
                {
                    MessageBox.Show("Error in binding listview");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddExecute(object? obj)
        {
            try
            {
                if (obj is Page page)
                {
                    page.NavigationService.Navigate(App.Container.GetInstance<AddCategoryPage>());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
