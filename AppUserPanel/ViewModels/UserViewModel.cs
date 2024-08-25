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

        public ObservableCollection<Product> Products { get; set; }
        public ICommand BuyCommand { get; }
        public ICommand BackCommand { get; }

        public UserViewModel()
        {
            dbContext = new();
            Products = new ObservableCollection<Product>(/* Load your products here */);
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
                                .ThenInclude(x => x.Photo)
                                .FirstOrDefault(x => x.UserId == PasswordHasher.UserId);

            if (cart != null && cart.Items != null)
            {
                Products = new ObservableCollection<Product>(cart.Items);
            }
            else
            {
                Products = new ObservableCollection<Product>();
            }
        }

    }
}
