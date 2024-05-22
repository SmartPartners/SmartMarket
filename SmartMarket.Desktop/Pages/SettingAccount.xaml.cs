using SmartMarket.Desktop.Components;
using SmartMarket.Desktop.Windows.AccountSettings;
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
        public SettingAccount()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshAsync();
        }

        public async Task RefreshAsync()
        {
            stPanel.Children.Clear();
            for (int i = 0; i < 10; i++)
            {
                AccountCompanent accountCompanent = new AccountCompanent();
                stPanel.Children.Add(accountCompanent);
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
