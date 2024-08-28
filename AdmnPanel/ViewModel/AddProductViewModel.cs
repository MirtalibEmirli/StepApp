using AdmnPanel.Pages;
using AppAdminPanel.Commands;
using AppAdminPanel.ViewModel;
using AppLibrary.Data;
using AppLibrary.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdmnPanel.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {
        private string _productName;
        private decimal _productPrice;
        private int _productQuantity;
        private Category _selectedCategory;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<PhotoProduct> _photos;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged( );
            }
        }

        public decimal ProductPrice
        {
            get => _productPrice;
            set
            {
                _productPrice = value;
                OnPropertyChanged( );
            }
        }

        public int ProductQuantity
        {
            get => _productQuantity;
            set
            {
                _productQuantity = value;
                OnPropertyChanged( );
            }
        }

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged( );
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

        public ObservableCollection<PhotoProduct> Photos
        {
            get => _photos ??= new ObservableCollection<PhotoProduct>();
            set
            {
                _photos = value;
                OnPropertyChanged(nameof(Photos));
            }
        }

        public ICommand AddCommand { get; }
       
        public ICommand AddPhotoCommand { get; }

        public AddProductViewModel()
        {
            LoadCategories();  
            AddCommand = new RelayCommand(AddProduct);
            BackCommand = new RelayCommand(BackCommandExecute);
            AddPhotoCommand = new RelayCommand(AddPhoto);
        }

        private void LoadCategories()
        {
            using (var dbContext = new MirtalibDbContext())
            {
                Categories = new ObservableCollection<Category>(dbContext.Categories.ToList());
            }
        }

        private void AddProduct(object? obj)
        {
            if (SelectedCategory != null && !string.IsNullOrWhiteSpace(ProductName)&&obj is Page page)
            {
                using (var dbContext = new MirtalibDbContext())
                {
                    if (!dbContext.Products.Any(p => p.Name == ProductName))
                    {
                        var product = new Product
                        {
                            Name = ProductName,
                            Price = ProductPrice,
                            Quantity = ProductQuantity,
                            CategoryId = SelectedCategory?.Id ?? 1,
                        };
                        dbContext.Products.Add(product);
                        dbContext.SaveChanges();

                        foreach (var photo in Photos)
                        {
                            photo.ProductId = product.Id;
                            dbContext.PhotoProducts.Add(photo);
                        }

                        dbContext.SaveChanges();

                        //cleaning
                        product = new Product();
                        _productName = string.Empty;
                        _productPrice = 0;
                        _productQuantity = 0;
                        SelectedCategory = new Category();
                        Photos = new ObservableCollection<PhotoProduct>();
                        System.Windows.MessageBox.Show("Product added successfully!");
                        page.NavigationService.Navigate(new Dashboard());
                        //burda problem var add edirem amma ondan sora propertyler bosalmr
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Please select a category and fill in all required fields!");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("A product with the same name already exists!");

            }
        }

        private void AddPhoto(object? obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                var photo = new PhotoProduct
                {
                    Bytes = imageBytes,
                    FileExtension = Path.GetExtension(openFileDialog.FileName),
                    Description = "Product Photo", // Opsiyonel: bir açıklama ekleyin
                    Size = imageBytes.Length / 1024m, // KB olarak boyut
                };
                Photos.Add(photo);
            }
        }
    }
}
