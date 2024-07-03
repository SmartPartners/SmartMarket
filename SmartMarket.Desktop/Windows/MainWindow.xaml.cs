using SmartMarket.Desktop.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void butt_shop_details_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rbMain_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new MainPage();
        }

        private void rbKassa_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new SalePage();
        }

        private void rbSalesDetails_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new SaleDetails();
        }

        private void rbWorkers_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new WorkersPage();
        }

        private void rbPartners_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new PartnersPage();
        }

        private void rbAccount_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new SettingAccount();
        }

        private void rbStoredetails_Click(object sender, RoutedEventArgs e)
        {
            this.PageNavigator.Content = new ShopDetailPage();
        }

        private void btnMaximized_Click(object sender, RoutedEventArgs e)
        {

            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else WindowState = WindowState.Maximized;
        }

        private void btnMinimized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void rbStoreKassa_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.Content = new KassaPage();
        }

        private void rbSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}