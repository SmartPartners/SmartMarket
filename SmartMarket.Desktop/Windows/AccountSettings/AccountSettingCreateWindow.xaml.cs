using SmartMarket.Desktop.Service.Interfrace.Users;
using SmartMarket.Desktop.Service.Service.Users;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
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
        private IUserService _userService;

        public AccountSettingCreateWindow()
        {
            InitializeComponent();
            this._userService = new UserService();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();            
        }

        private async void btnCreateAccount_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "" && txtPhoneNumber.Text != "" && txtOylik.Text != "" && txtPassword.Text != "" && comboxRole.Text != "")
            {
                UserForCreationDto userForCreationDto = new UserForCreationDto()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Role = (UserRole)comboxRole.SelectedIndex,
                    IsActive = true,
                    Oylik = decimal.Parse(txtOylik.Text),
                    Password = txtPassword.Text
                };

                var dbResult = await _userService.CreateAsync(userForCreationDto);

                if(dbResult != null) this.Close();                
                else MessageBox.Show("Yaratilda xato!");                
                
            } else MessageBox.Show("Hamma polyalarni kiriting!");


        }
    }
}
