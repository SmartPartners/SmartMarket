﻿using System;
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
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for ProductCreateDto.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        public ProductCreateWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
