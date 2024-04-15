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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для SaleDetails.xaml
    /// </summary>
    public partial class SaleDetails : Page
    {
        public SaleDetails()
        {
            InitializeComponent();
        }

        private void butt_sale_history_Click(object sender, RoutedEventArgs e)
        {
            this.leftGrid.RowDefinitions[1].Height = GridLength.Auto;
            this.left_bottom_panel_sale_history.Visibility = Visibility.Visible;
            this.left_bottom_panel_return.Visibility = Visibility.Collapsed;
            this.left_bottom_panel_invalid.Visibility = Visibility.Collapsed;

            this.content_menu.Content = new SaleHistory();
        }

        private void butt_top_sale_Click(object sender, RoutedEventArgs e)
        {
            this.leftGrid.RowDefinitions[1].Height = new GridLength(0);
            this.content_menu.Content = new TopSale();
        }

        private void butt_return_prods_Click(object sender, RoutedEventArgs e)
        {
            this.leftGrid.RowDefinitions[1].Height = GridLength.Auto;
            this.left_bottom_panel_sale_history.Visibility = Visibility.Collapsed;
            this.left_bottom_panel_return.Visibility = Visibility.Visible;
            this.left_bottom_panel_invalid.Visibility = Visibility.Collapsed;

            this.content_menu.Content = new ReturnProductsPage();
        }

        private void butt_invalid_prods_Click(object sender, RoutedEventArgs e)
        {
            this.leftGrid.RowDefinitions[1].Height = GridLength.Auto;
            this.left_bottom_panel_sale_history.Visibility = Visibility.Collapsed;
            this.left_bottom_panel_return.Visibility = Visibility.Collapsed;
            this.left_bottom_panel_invalid.Visibility = Visibility.Visible;

            this.content_menu.Content = new InvalidProductsPage();
        }
    }
}
