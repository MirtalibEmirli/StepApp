using AppLibrary.Data;
using AppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUserPanel.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<Order> _orders;
        private readonly MirtalibDbContext _dbContext;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        public OrdersViewModel(int userId)
        {
            _dbContext = new MirtalibDbContext();
            LoadOrders(userId);
        }

        private void LoadOrders(int userId)
        {
            var userOrders = _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Products)
                .ThenInclude(p => p.Photo)
                .ToList();

            Orders = new ObservableCollection<Order>(userOrders);
        }
    }
}