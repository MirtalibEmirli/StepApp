using AppLibrary.Data;
using AppLibrary.Models;
using AppUserPanel.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppUserPanel.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private MirtalibDbContext dbContext;

        public ObservableCollection<Product> Products { get; set; }

        public ICommand LikeCommand { get; }
        public ICommand UserCommand { get; }
        public ICommand CartCommand { get; }
        public ICommand OutCommand { get; }


        public DashboardViewModel()
        {


            dbContext = new();
            LikeCommand = new RelayCommand(ExecuteLikeCommand);
            UserCommand = new RelayCommand(ExecuteUserCommand);
            CartCommand = new RelayCommand(ExecuteCartCommand);
            OutCommand = new RelayCommand(ExecuteOutCommand);
            LoadProducts();
        }

        private void ExecuteOutCommand(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCartCommand(object? obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteLikeCommand(object? obj)
        {
            throw new NotImplementedException();
        }

        private void LoadProducts()
        {
            var datass = dbContext.Products.Include(x => x.Photo).ToList();
            Products = new ObservableCollection<Product>(datass);
        }




        private void ExecuteUserCommand(object? obj)
        {
        }


    }
}
