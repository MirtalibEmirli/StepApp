using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using AppUserPanel.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace AppUserPanel.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private MirtalibDbContext dbContext;
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products { get { return _products; } set { _products = value; OnPropertyChanged(); } }

        public ICommand LikeCommand { get; }
        public ICommand UserCommand { get; }
        public ICommand CartCommand { get; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }
        public DashboardViewModel()
        {


            dbContext = new();
            LikeCommand = new RelayCommand(ExecuteLikeCommand);
            UserCommand = new RelayCommand(ExecuteUserCommand);
            CartCommand = new RelayCommand(ExecuteCartCommand);
            BackCommand = new RelayCommand(BackCommandExecute);
            LoadProducts();
        }



        private void ExecuteCartCommand(object? obj)
        {
            var cart = dbContext.Carts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
            if(cart == null)
            {
                Cart newCart = new Cart
                {
                    UserId = PasswordHasher.UserId
                };
                dbContext.Carts.Add(newCart);
                dbContext.SaveChanges();
                cart = newCart;
            }
            List<Product> cartItems = new();
            if(cart.Items != null)
            cartItems.AddRange(cart.Items);
            cartItems.Add(SelectedProduct);
            cart.Items = cartItems;
            //SelectedProduct.Carts.ToList().Add(cart);
            dbContext.SaveChanges();
                MessageBox.Show(SelectedProduct.Name);

             

        }

        private void ExecuteLikeCommand(object? obj)
        {

        }

        private void LoadProducts()
        {
            var datass = dbContext.Products.Include(x => x.Photo).ToList();
            Products = new ObservableCollection<Product>(datass);
        }




        private void ExecuteUserCommand(object? obj)
        {
            if (obj is Page page)
            {
                page.NavigationService.Navigate(App.Container.GetInstance<UserPage>());
            }
        }


    }
}
