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

namespace SmartMarket.Desktop.Windows.AccountSettings
{
    /// <summary>
    /// Interaction logic for AccountSettingCreateWindow.xaml
    /// </summary>
    public partial class AccountSettingCreateWindow : Window
    {
        public AccountSettingCreateWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
