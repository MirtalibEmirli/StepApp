using AppAdminPanel.Commands;
using AppAdminPanel.Services;
using AppAdminPanel.ViewModel;
using AppLibrary.Data;
using AppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdmnPanel.ViewModel
{
   public class AddCategoryPageViewModel:BaseViewModel
    {
        MirtalibDbContext context;
        public ICommand AddCommand { get; set; }
        private string nametxt { get; set; }
        public string Nametxt {
            get { return nametxt; }
            set { nametxt = value;OnPropertyChanged(); }
        }

        public AddCategoryPageViewModel()
        {
            AddCommand = new RelayCommand(AddCommandExecute);
            context   = new MirtalibDbContext();
            BackCommand = new RelayCommand(BackCommandExecute);
        }

        private void AddCommandExecute(object? obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(Nametxt))
                {
                    var existingCategory = context.Categories.FirstOrDefault(c => c.Name == Nametxt);
                    if (existingCategory != null)
                    {
                        MessageBox.Show($"The category '{Nametxt}' already exists. Please choose a different name.");
                        return;
                    }

                    var category = new Category
                    {
                        Name = Nametxt
                    };
                    context.Categories.Add(category);
                    context.SaveChanges();
                    MessageBox.Show($"{Nametxt} added successfully.");
                    Nametxt = "";
                }
                else
                {
                    MessageBox.Show("Invalid name");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
