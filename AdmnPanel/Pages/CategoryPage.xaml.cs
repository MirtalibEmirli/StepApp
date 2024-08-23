﻿using AdmnPanel.ViewModel;
using AppAdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdmnPanel.Pages
{
    
    public partial class CategoryPage : Page
    {
        public CategoryPage()
        {
            InitializeComponent();
            DataContext = App.Container.GetInstance<CategoryViewModel>();
        }
    }
}
