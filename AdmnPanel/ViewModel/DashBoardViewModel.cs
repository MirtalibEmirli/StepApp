using AdmnPanel.Pages;
using AppAdminPanel;
using AppAdminPanel.Commands;
using AppAdminPanel.Services;
using AppLibrary.Data;
using AppLibrary.Models;
using AppAdminPanel.Services;
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
using AppAdminPanel.ViewModel;

namespace AdmnPanel.ViewModel
{
    public class DashboardViewModel : BaseViewModel
    {
        public ICommand CategoryCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand DashboardCommand { get; set; }
        public ICommand UsersCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        MirtalibDbContext dbContext;
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }
        public DashboardViewModel()
        {
            CategoryCommand = new RelayCommand(CategoryCommandExecute);
            AddProductCommand = new RelayCommand(AddProductExecute);
            dbContext = new MirtalibDbContext();
            UsersCommand = new RelayCommand(UsersExecute);
            BackCommand = new RelayCommand(BackCommandExecute);
            DashboardCommand = new RelayCommand(RefreshDashboard);
            DeleteCommand = new RelayCommand(DeleteExecute);
            LoadProducts();
        }

        private void DeleteExecute(object? obj)
        {
          var result=  MessageBox.Show("Bu Mehsulu silmek isdeyirsiz?","Confirm",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (result==MessageBoxResult.Yes)
            {
                dbContext.Products.Remove(SelectedProduct);
                dbContext.SaveChanges();
                LoadProducts();
                MessageBox.Show("Product silindi");
            }
            
        }

        private void RefreshDashboard(object? obj)
        {
            LoadProducts();

        }

        private void UsersExecute(object? obj)
        {
            if (obj is Page page)
            {
                page.NavigationService.Navigate(new UserPage());
            }
        }

         
        private void LoadProducts()
        {

            var datass = dbContext.Products.Include(x => x.Photo).Include(x=> x.Category).ToList();
            Products = new ObservableCollection<Product>(datass);
        }

        private void AddProductExecute(object? obj)
        {
            if (obj is Page page)
            {
                page.NavigationService.Navigate(new AddProductPage());
            }
        }

        private void CategoryCommandExecute(object? obj)
        {
            try
            {
                if (obj is Page page && page.NavigationService != null)
                {
                    page.NavigationService.Navigate(new CategoryPage());

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
