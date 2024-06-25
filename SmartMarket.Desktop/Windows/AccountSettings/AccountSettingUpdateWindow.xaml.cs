using SmartMarket.Domin.Entities.Users;
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
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Windows.AccountSettings
{
    /// <summary>
    /// Interaction logic for AccountSettingUpdateWindow.xaml
    /// </summary>
    public partial class AccountSettingUpdateWindow : Window
    {
        public AccountSettingUpdateWindow(User user)
        {
            InitializeComponent();

            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;   
            txtPhoneNumber.Text = user.PhoneNumber;
            txtRole.Text = user.Role.ToString();
            txtOylik.Text = user.Oylik.ToString();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
