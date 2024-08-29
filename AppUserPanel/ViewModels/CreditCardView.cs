using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AppUserPanel.ViewModels
{
    public class CreditCardViewModel : BaseViewModel
    {
        private CreditCard _newCreditCard;
        private User _currentUser;
        private MirtalibDbContext _context;

        public CreditCard NewCreditCard
        {
            get => _newCreditCard;
            set { _newCreditCard = value; OnPropertyChanged(); }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); }
        }
         
        public ICommand AddCardCommand { get; }

        public CreditCardViewModel(int id)
        {
            _context = new MirtalibDbContext();
            CurrentUser = _context.Users
                .Include(u => u.Photo)
                .FirstOrDefault(a => a.Id == id) ?? new User();
            NewCreditCard = new CreditCard();
            AddCardCommand = new RelayCommand(AddCard);
          
        }

        private void AddCard(object? obj)
        {
            if (IsValidCard(NewCreditCard))
            {
                var existingCard = _context.CreditCarts.FirstOrDefault(c => c.UserId == CurrentUser.Id);
                if (existingCard != null)
                {
                    existingCard.money += NewCreditCard.money;
                    _context.CreditCarts.Update(existingCard);
                }
                else
                {
                    NewCreditCard.UserId = CurrentUser.Id;
                    _context.CreditCarts.Add(NewCreditCard);
                }
                _context.SaveChanges();
                MessageBox.Show("Money added successfully.");
                NewCreditCard = new CreditCard(); // Reset form
            }
            else
            {
                // Display validation error
            }
        }

        private bool IsValidCard(CreditCard card)
        {
            // Add validation logic here (e.g., check if the card number, expiration date, and CVV are valid)
            return true;
        }
    }

}
