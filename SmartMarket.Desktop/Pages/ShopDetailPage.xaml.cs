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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for ShopDetailPage.xaml
    /// </summary>
    public partial class ShopDetailPage : Page
    {
        public ShopDetailPage()
        {
            InitializeComponent();
        }

        private void rbInvalidProds_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rbReturnProds_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rbExpence_Click(object sender, RoutedEventArgs e)
        {
            this.content_menu.Content = new ExpensesPage();
        }
    }
}
