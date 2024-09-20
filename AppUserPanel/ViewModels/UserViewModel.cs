using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;
using System.Windows.Interop;
using Document = iTextSharp.text.Document;
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
        decimal total;
        string name;
        PhotoProduct photoProduct;
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


            var result = MessageBox.Show("Bir product ucun No ,secdiyinizin  hamsini silmek ucun Yes ", "Information", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

            if (cart == null)
            {
              
                MessageBox.Show("Cart not found.");
                return;
            }

            var result = MessageBox.Show("Do you want to buy this product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var usersCart = dbContext.Carts.FirstOrDefault(x => x.UserId == PasswordHasher.UserId);
                var buyingProduct = dbContext.CartProducts.FirstOrDefault(x => x.ProductId == product.Id && x.CartId == usersCart.Id);

                if (buyingProduct == null)
                {
                    MessageBox.Show("Product not found in cart.");
                    return;
                }

                var totalAmount = product.Price * buyingProduct.Quantity;

                if (cart.money < totalAmount)
                {
                    MessageBox.Show("NoMoney broi", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                photoProduct = buyingProduct.Product.Photo;
                cart.money -= totalAmount;
                dbContext.CreditCarts.Update(cart);
                total = totalAmount;
                name = SelectedProduct.Name;
                var order = new OrderItem
                {
                    ProductId = product.Id,
                    UserId = PasswordHasher.UserId,
                    Quantity = buyingProduct.Quantity
                };
                dbContext.OrderItems.Add(order);

                dbContext.CartProducts.Remove(buyingProduct);
                dbContext.SaveChanges();

                MessageBox.Show($"You have successfully bought {buyingProduct.Quantity} {product.Name}(s).");
                Products = new();
                LoadProducts();

                SendVerificationCode();

            }
        }


        private void SendVerificationCode()
        {
            Thread thread = new Thread(() =>
            {
                var context = new MirtalibDbContext();
                var NewUser = context.Users.FirstOrDefault(x => x.Id == PasswordHasher.UserId);
                string senderEmail = "mirtalibemirli498@gmail.com";
                string senderPassword = "eiwk gysv kmwd tttx";
                string recipientEmail = NewUser.Email;
                string code = @$"{total} $ you paid for {name} in our Store, Thank you :)";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string pdfFilePath = "Bill.pdf";
                string imageFilePath = "ProductImage.jpg";

               
                GeneratePdf(pdfFilePath, code);
 
                SaveProductImage(photoProduct, imageFilePath);

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    using (MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail))
                    {
                        mailMessage.Subject = "Bill You Get from Mr Market :)";
                        mailMessage.Body = @$"{total} $ you paid for {name} product in our Store, Thank you :)";

                        if (File.Exists(pdfFilePath))
                        {
                            mailMessage.Attachments.Add(new Attachment(pdfFilePath));
                        }

                        if (File.Exists(imageFilePath))
                        {
                            mailMessage.Attachments.Add(new Attachment(imageFilePath));
                        }

                        smtpClient.Send(mailMessage);
                    }
                }

                // Cleanup files
                if (File.Exists(pdfFilePath))
                {
                    File.Delete(pdfFilePath);
                }

                if (File.Exists(imageFilePath))
                {
                    File.Delete(imageFilePath);
                }
            });

            thread.Start();
        }



        private void GeneratePdf(string filePath, string Code)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (iTextSharp.text.Document document = new iTextSharp.text.Document())
                {
                    PdfWriter.GetInstance(document, fs);
                    document.Open();
                    document.Add(new iTextSharp.text.Paragraph($"Your Bill is {Code}"));
                    document.Close();
                }
            }
        }
        private void SaveProductImage(PhotoProduct photoProduct, string filePath)
        {
            if (photoProduct != null && photoProduct.Bytes != null)
            {
                File.WriteAllBytes(filePath, photoProduct.Bytes);
            }
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
