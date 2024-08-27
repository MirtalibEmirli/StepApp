using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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


        public UserViewModel()
        {
            dbContext = new();
            Products = new ObservableCollection<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Salam",
                    Price = 12
                }
            };
            BuyCommand = new RelayCommand(BuyProduct, CanExecuteBuyCommand);
            BackCommand = new RelayCommand(BackCommandExecute);
            LoadProducts();
        }

        private bool CanExecuteBuyCommand(object parameter)
        {
            return SelectedProduct != null;
        }

        private void BuyProduct(object parameter)
        {
             
        }

        private void LoadProducts()
        {
            var cart = dbContext.Carts
                                .Include(x => x.Items)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Photo)
                                .FirstOrDefault(x => x.UserId == PasswordHasher.UserId);

            if (cart != null && cart.Items != null)
            {
                Products.Clear(); 
                foreach (var item in cart.Items)
                {
                    var product = dbContext.Products
                                           .Include(p => p.Photo)
                                           .FirstOrDefault(x => x.Id == item.ProductId);
                    product.Quantity = item.Quantity;
                    if (product != null)
                    {
                        Products.Add(product);
                    }
                }
            }
            
        }


    }
}
