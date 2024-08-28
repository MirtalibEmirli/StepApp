using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using AppUserPanel.Pages;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace AppUserPanel.ViewModels;

public class ProfilViewModel : BaseViewModel
{
    private User user;
    public User User { get { return user; } set { user = value;OnPropertyChanged(); } }

    public ICommand ChangePhotoCommand { get; set; }
    public ICommand ChangeUsernameCommand { get; set; }
    public ICommand ChangeEmailCommand { get; set; }
    public ICommand ViewOrdersCommand { get; set; }
    public ICommand AddCreditCardCommand { get; set; }
    public ICommand ViewCreditCardsCommand { get; set; }
    public ICommand DashBoardCommand { get; set; }
    MirtalibDbContext context;
    private Page currentView;
    public Page CurrentView
    {
        get
        {
            return currentView;
        }
        set
        {
            currentView = value; OnPropertyChanged();
        }
    }

    public ProfilViewModel()
    {
        try
        {
            context = new();
            ChangePhotoCommand = new RelayCommand(ChangePhoto);
            ChangeUsernameCommand = new RelayCommand(ChangeUsername,IsChangeName);
            ChangeEmailCommand = new RelayCommand(ChangeEmail);
            ViewOrdersCommand = new RelayCommand(ViewOrders);
            AddCreditCardCommand = new RelayCommand(AddCreditCard);
            ViewCreditCardsCommand = new RelayCommand(ViewCreditCards);
            DashBoardCommand = new RelayCommand(DasboardExecute);
            BackCommand = new RelayCommand(BackCommandExecute);
            User = context.Users.FirstOrDefault(a => PasswordHasher.UserId == a.Id)?? new User();

            CurrentView = new DefaultView();
        }
        catch (Exception ex)
        {
            // Log or display the error
            System.Windows.MessageBox.Show($"Error loading dashboard: {ex.Message}");
        }
    }

    private bool IsChangeName(object? obj)
    {

        if (User.UserName.Length> 3)
        {
            return true;
        }
       
        return false;
    }

    private void DasboardExecute(object? obj)
    {

    }

    private void ChangePhoto(object obj)
    {


    }
    private void AddPhoto(object? obj)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
        };

        if (openFileDialog.ShowDialog() == true)  // Corrected line
        {
            byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
            var photo = new PhotoProduct
            {
                Bytes = imageBytes,
                FileExtension = Path.GetExtension(openFileDialog.FileName),
                Description = "Product Photo",
                Size = imageBytes.Length / 1024m,
            };

            // You can now proceed to add the photo to your collection or save it as needed
        }
    }

    private void ChangeUsername(object obj)
    {


        context.Users.Update(User);
        context.SaveChanges();
    }

    private void ChangeEmail(object obj)
    {
        // Implement logic to change email
    }

    private void ViewOrders(object obj)
    {
        // Implement logic to display previous orders
        // CurrentView = new OrdersView();
    }

    private void AddCreditCard(object obj)
    {
        // Implement logic to add a new credit card
        // CurrentView = new AddCreditCardView();
    }

    private void ViewCreditCards(object obj)
    {
        // Implement logic to display credit cards
        //CurrentView = new CreditCardsView();
    }
}
