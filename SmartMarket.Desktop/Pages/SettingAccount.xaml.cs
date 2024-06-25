using SmartMarket.Desktop.Components;
using SmartMarket.Desktop.Service.Interfrace.Users;
using SmartMarket.Desktop.Service.Service.Users;
using SmartMarket.Desktop.Windows.AccountSettings;
using SmartMarket.Domin.Configurations;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for SettingAccount.xaml
    /// </summary>
    public partial class SettingAccount : Page
    {
        private IUserService _userService;

        public SettingAccount()
        {
            InitializeComponent();
            this._userService = new UserService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            stPanel.Children.Clear();

            var users = await _userService.GetAllAsync();
            
            if(users != null)
            {
                foreach ( var user in users)
                {
                    AccountCompanent accountCompanent = new AccountCompanent();
                    accountCompanent.SetaData(user);
                    stPanel.Children.Add(accountCompanent);
                }
            }
        }


        public void ShowEffectWindow()
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = 50; // You can adjust the blur radius as needed
            Effect = blur;
        }
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AccountSettingCreateWindow window = new AccountSettingCreateWindow();
            ShowEffectWindow();
            window.ShowDialog();
            Effect = null;
        }
    }
}
