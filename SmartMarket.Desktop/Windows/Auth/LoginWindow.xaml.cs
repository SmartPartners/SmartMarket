using SmartMarket.Service.DTOs.Auth;
using SmartMarket.Service.Helpers;
using SmartMarket.Service.Interfrace.Auth;
using SmartMarket.Service.Service.Auth;
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

namespace SmartMarket.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAuthService authService;
        public LoginWindow()
        {
            InitializeComponent();
            this.authService = new AuthService();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private async void btnLogin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var phone = txtPhone.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(phone)) 
            {
                MessageBox.Show("Telefon raqam kiritilmadi");
                return;
            }

            if (string.IsNullOrEmpty(password)) 
            {
                MessageBox.Show("Parol kiritilmadi");
                return;
            }

            var loginDTO = new LoginDTO()
            {
                Password = password,
                PhoneNumber = phone
            };

            var result = await authService.LoginAsync(loginDTO);
            if (result != null && result.Data != "")
            {
                TokenHelper.apiToken = result.Data;
                
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Foydalanuvchi topilmadi");
                return;
            }
        }
    }
}
