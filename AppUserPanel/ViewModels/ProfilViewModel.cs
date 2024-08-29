using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using AppUserPanel.Pages;
using Microsoft.EntityFrameworkCore;
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

            User = context.Users
                        .Include(u => u.Photo)
                        .FirstOrDefault(a => PasswordHasher.UserId == a.Id)??new User();

            CurrentView = new DefaultView();
        }
        catch (Exception ex)
        {
     
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
        CurrentView = new DefaultView();
    }

    private void ChangePhoto(object obj)
    {

        //bax
        using (var openFileDialog = new OpenFileDialog
        {
            Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
        })
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                var photo = new PhotoUser
                {
                    Bytes = imageBytes,
                    FileExtension = Path.GetExtension(openFileDialog.FileName),
                    Description = $"{User.UserName} Photo",
                    Size = imageBytes.Length / 1024m,
                    UserId = User.Id
                };

                User.Photo = photo;

                context.Users.Update(User);
                context.SaveChanges();

                User = context.Users
                              .Include(u => u.Photo)
                              .FirstOrDefault(u => u.Id == User.Id) ?? new User();

                OnPropertyChanged(nameof(User));
                context.SaveChanges();

            }
        }
    }





    private void ChangeUsername(object obj)
    {


        context.Users.Update(User);
        context.SaveChanges();
    }

    private void ChangeEmail(object obj)
    {
        CurrentView = new ChangeMailPage();
    }

    private void ViewOrders(object obj)
    {
        CurrentView = new Orders(); 
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
