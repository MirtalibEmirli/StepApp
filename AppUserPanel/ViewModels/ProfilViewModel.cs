using AppLibrary.Models;
using AppUserPanel.Commands;
using AppUserPanel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppUserPanel.ViewModels;

public class ProfilViewModel : BaseViewModel
{
    public User user { get; set; }
    public User User { get { return user; } set { user = value; } }
    public ICommand ChangePhotoCommand { get; set; }
    public ICommand ChangeUsernameCommand { get; set; }
    public ICommand ChangeEmailCommand { get; set; }
    public ICommand ViewOrdersCommand { get; set; }
    public ICommand AddCreditCardCommand { get; set; }
    public ICommand ViewCreditCardsCommand { get; set; }
    public ICommand DashBoardCommand { get; set; }
    private Page currentView;
    public Page CurrentView {
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
            ChangePhotoCommand = new RelayCommand(ChangePhoto);
        ChangeUsernameCommand = new RelayCommand(ChangeUsername);
        ChangeEmailCommand = new RelayCommand(ChangeEmail);
        ViewOrdersCommand = new RelayCommand(ViewOrders);
        AddCreditCardCommand = new RelayCommand(AddCreditCard);
        ViewCreditCardsCommand = new RelayCommand(ViewCreditCards);
        DashBoardCommand = new RelayCommand(DasboardExecute);

        // Initialize with default view
        ///errror
       
            CurrentView = new DefaultView();
        }
        catch (Exception ex)
        {
            // Log or display the error
            MessageBox.Show($"Error loading dashboard: {ex.Message}");
        }
    }

    private void DasboardExecute(object? obj)
    {
       
    }

    private void ChangePhoto(object obj)
    {
        // Implement logic to change photo
    }

    private void ChangeUsername(object obj)
    {
        // Implement logic to change username
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
