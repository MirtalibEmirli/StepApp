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
        private Category _selectedCategory;
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ICommand ProfilCommand { get; }
        public ICommand UserCommand { get; }
        public ICommand CartCommand { get; }
        public ICommand LoadCategory { get; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }
        public DashboardViewModel()
        {

            LoadCategory = new RelayCommand(ExecuteLoadCategory);
            dbContext = new();
            ProfilCommand = new RelayCommand(ExecuteProfilCommand);
            UserCommand = new RelayCommand(ExecuteUserCommand);
            CartCommand = new RelayCommand(ExecuteCartCommand);
            BackCommand = new RelayCommand(BackCommandExecute);
            LoadProducts();
        }

        private void ExecuteLoadCategory(object? obj)
        {
            LoadProducts();
        }

        private void ExecuteCartCommand(object? obj)
        {
            try
            {
                if (SelectedProduct !=null)
                {
                    var cart = dbContext.Carts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
                    if (cart == null)
                    {
                        Cart newCart = new Cart
                        {
                            UserId = PasswordHasher.UserId
                        };
                        dbContext.Carts.Add(newCart);
                        dbContext.SaveChanges();
                        cart = dbContext.Carts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
                    }
                    var product = SelectedProduct;
                    var cartProduct = dbContext.CartProducts.FirstOrDefault(x => x.ProductId == product.Id && x.CartId == cart.Id);
                    if (cartProduct is null)
                    {
                        cartProduct = new CartProduct
                        {
                            CartId = cart.Id,
                            ProductId = product.Id,
                            Quantity = 1
                        };
                        dbContext.CartProducts.Add(cartProduct);
                    }
                    else
                    {
                        cartProduct.Quantity++;
                    }
                    dbContext.SaveChanges();
                    MessageBox.Show($"{SelectedProduct.Name} added your cart successfully!");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          

             

        }

        private void ExecuteProfilCommand(object? obj)
        {
            try
            {
                if (obj is Page page)
                {
                    page.NavigationService.Navigate(new ProfilPage());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void LoadProducts()
        {
            Categories = new ObservableCollection<Category>(dbContext.Categories.ToList());
            if(SelectedCategory == null)
            {
                var datass = dbContext.Products.Include(x => x.Photo).Include(x=> x.Category).ToList();
                Products = new ObservableCollection<Product>(datass);
            }
            else
            {
                var datass = dbContext.Products.Include(x => x.Photo).Include(x => x.Category).Where(x => x.CategoryId == SelectedCategory.Id).ToList();
                Products = new ObservableCollection<Product>(datass);
            }
        }




        private void ExecuteUserCommand(object? obj)
        {
            if (obj is Page page)
            {
                page.NavigationService.Navigate(new UserPage());
            }
        }


    }
}
