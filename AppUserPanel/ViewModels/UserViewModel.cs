using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Security.Policy;
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
            var product = SelectedProduct;


            var result = MessageBox.Show("Bir product ucun No ,secdiyinizin  hamsini almaqcun Yes ", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            var usersCart = dbContext.Carts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
            var buyingProduct = dbContext.CartProducts.FirstOrDefault(x => x.ProductId == product.Id && x.CartId == usersCart.Id);


            if (result == MessageBoxResult.No)
            {
                buyingProduct.Quantity = buyingProduct.Quantity - 1;
                dbContext.CartProducts.Update(buyingProduct);



                dbContext.SaveChanges();

                MessageBox.Show($"{buyingProduct.Product.Name} dan 1 eded sildiniz ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            else
            {
                dbContext.Remove(buyingProduct);


                dbContext.SaveChanges();
                MessageBox.Show($"{buyingProduct.Product.Name} dan {buyingProduct.Quantity} qeder sildiniz ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            Products = new();
            LoadProducts();
        }

        private bool CanExecuteBuyCommand(object parameter)
        {
            return SelectedProduct != null;
        }

        private void BuyProduct(object parameter)
        {
            var product = SelectedProduct;
            var cart = dbContext.CreditCarts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
            
             
            var result = MessageBox.Show("Bir product ucun No ,secdiyinizin  hamsini almaqcun Yes ", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
            var usersCart = dbContext.Carts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
            var buyingProduct = dbContext.CartProducts.FirstOrDefault(x => x.ProductId == product.Id && x.CartId == usersCart.Id);

            OrderItem order = new();
            order.ProductId = product.Id;
            order.UserId = PasswordHasher.UserId;

            if (result == MessageBoxResult.No)
            {
                if (cart.money < product.Price)
                {
                    MessageBox.Show("Balansinizda kifayet qeder vesait yoxdur", "Xeta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                buyingProduct.Quantity = buyingProduct.Quantity - 1;
                    dbContext.CartProducts.Update(buyingProduct);


                order.Quantity = 1;
                dbContext.OrderItems.Add(order);

                dbContext.SaveChanges();

                MessageBox.Show($"{buyingProduct.Product.Name} dan 1 eded aldiniz ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                var cash = buyingProduct.Product.Price;
                cart.money -=cash;
            }
            else
            {
                if (cart.money < product.Price* buyingProduct.Quantity)
                {
                    MessageBox.Show("Balansinizda kifayet qeder vesait yoxdur", "Xeta", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                dbContext.Remove(buyingProduct);
                var cash = buyingProduct.Product.Price;
                cart.money -= cash*buyingProduct.Quantity;
                dbContext.CreditCarts.Update(cart);

                order.Quantity = buyingProduct.Quantity;
                dbContext.OrderItems.Add(order);

                dbContext.SaveChanges();
                MessageBox.Show($"{buyingProduct.Product.Name} dan {buyingProduct.Quantity} qeder aldiniz ", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            Products = new();
            LoadProducts();
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
