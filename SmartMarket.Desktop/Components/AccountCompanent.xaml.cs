using SmartMarket.Desktop.Windows.AccountSettings;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Components
{
    /// <summary>
    /// Interaction logic for AccountCompanent.xaml
    /// </summary>
    public partial class AccountCompanent : UserControl
    {
        private User user;

        public AccountCompanent()
        {
            InitializeComponent();
        }

        public void SetaData(User user)
        {
            this.user = user;
            lbName.Text = user.FirstName + " " + user.LastName;
            lbRole.Text = user.Role.ToString();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {            
            AccountSettingUpdateWindow window = new AccountSettingUpdateWindow(user);
            ShowEffectWindow();
            window.ShowDialog();
            Effect = null;
        }   

        public void ShowEffectWindow()
        {
            BlurEffect blur = new BlurEffect();
            blur.Radius = 50; // You can adjust the blur radius as needed
            Effect = blur;
        }
    }
}
