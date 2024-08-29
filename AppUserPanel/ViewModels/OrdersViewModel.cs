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
        private ObservableCollection<OrderItem> _orders;
        private readonly MirtalibDbContext _dbContext;

        public ObservableCollection<OrderItem> Orders
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
            var userOrders = _dbContext.OrderItems
                .Where(o => o.UserId == userId)
                .Include(o => o.Product)
                    .ThenInclude(p => p.Photo)
                .Include(o => o.Product)
                    .ThenInclude(p => p.Category) 
                .ToList();

            Orders = new ObservableCollection<OrderItem>(userOrders);
        }

    }
}