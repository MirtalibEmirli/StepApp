using AdmnPanel.Pages;
using AppAdminPanel;
using AppAdminPanel.Commands;
using AppAdminPanel.Services;
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
    public class DashboardViewModel : InotifyService
    {
        public ICommand CategoryCommand { get; set; }
        public ICommand AddProductCommand { get; set; }
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

        public DashboardViewModel()
        {
            CategoryCommand = new RelayCommand(CategoryCommandExecute);
            AddProductCommand = new RelayCommand(AddProductExecute);
            dbContext = new MirtalibDbContext();
            LoadProducts();
        }

        private void LoadProducts()
        {

            //Products = new ObservableCollection<Product>(dbContext.Products.ToList());
            var datass = dbContext.Products.Include(x=>x.Photo).ToList();
            Products = new ObservableCollection<Product>(datass);
        }

        private void AddProductExecute(object? obj)
        {
            if (obj is Page page)
            {
                page.NavigationService.Navigate(App.Container.GetInstance<AddProductPage>());
            }
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
