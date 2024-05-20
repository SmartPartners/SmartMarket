using SmartMarket.Desktop.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMarket.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for FinishingProductsPage.xaml
    /// </summary>
    public partial class FinishingProductsPage : Page
    {
        public FinishingProductsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            wrapPanelFinishingProducts.Children.Clear();
            for (int i = 0; i < 10; i++)
            {
                FinishingProductComponent accountCompanent = new FinishingProductComponent();
                wrapPanelFinishingProducts.Children.Add(accountCompanent);
            }
        }
    }
}
