using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppUserPanel.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private MirtalibDbContext dbContext;
        private Product _selectedProduct;
        public Product SelectedProduct 
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }

            set { products = value;OnPropertyChanged(); }
        }
        public ICommand BuyCommand { get; }
        public ICommand CancelCommand { get; }


        public UserViewModel()
        {
            dbContext = new();
            Products = new ObservableCollection<Product>();
            BuyCommand = new RelayCommand(BuyProduct, CanExecuteBuyCommand);
            BackCommand = new RelayCommand(BackCommandExecute);
            CancelCommand = new RelayCommand(Cancelexecute, IsExeCuteCancell);
            LoadProducts();
        }

        private bool IsExeCuteCancell(object? obj)
        {
            return true;
        }

        private void Cancelexecute(object? obj)
        {
            MessageBox.Show("fix me");
        }

        private bool CanExecuteBuyCommand(object parameter)
        {
            return SelectedProduct != null;
        }

        private void BuyProduct(object parameter)
        {
             ///
        }

        private void LoadProducts()
        {
            var cart = dbContext.Carts
                                .Include(x => x.Items)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Photo)
                                .FirstOrDefault(x => x.UserId == PasswordHasher.UserId);

            if (cart?.Items != null)
            {
                foreach (var item in cart.Items)
                {
                    var product = dbContext.Products
                                           .Include(p => p.Photo)
                                           .FirstOrDefault(x => x.Id == item.ProductId);

                    if (product != null)
                    {
                        product.Quantity = item.Quantity;
                        Products.Add(product);
                    }
                }
            }

            }


    }
}
