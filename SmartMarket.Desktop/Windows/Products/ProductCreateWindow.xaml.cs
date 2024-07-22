using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.Interfrace.Product;
using SmartMarket.Desktop.Service.Service.Categories;
using SmartMarket.Desktop.Service.Service.Products;
using SmartMarket.Desktop.Windows.Categories;
using SmartMarket.Desktop.Windows.CustomWindows;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.Products;
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

namespace SmartMarket.Desktop.Windows.Products
{
    /// <summary>
    /// Interaction logic for ProductCreateDto.xaml
    /// </summary>
    public partial class ProductCreateWindow : Window
    {
        private ICategoriesService _categoryService;
        private IProductService _productServise;

        public ProductCreateWindow()
        {
            InitializeComponent();
            this._categoryService = new CategoriesService();
            this._productServise = new ProductService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetAllCategories();
        }

        public async Task GetAllCategories()
        {
            var categories = await _categoryService.GetcategoriesAsync();
            comboCategory.ItemsSource = categories;
        }
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(txtBarCode.Text.Length == 0)
            {
                MyMessage.Show("Barkodni kiriting!");
                return;
            }
            if (txtProductName.Text.Length == 0)
            {
                MyMessage.Show("Mahsulot nomini kiriting!");
                return;
            }
            if (comboCategory.SelectedValue == null || (int)comboCategory.SelectedValue == 0)
            {
                MyMessage.Show("Kategoriyani tanlang!");
                return;
            }
            if (txtQuantity.Text.Length == 0)
            {
                MyMessage.Show("Mahsulot miqdorini kiriting!");
                return;
            }
            if (txtPrice.Text.Length == 0 || double.Parse(txtPrice.Text)== 0)
            {
                MyMessage.Show("Mahsulot narxini kiriting!");
                return;
            }
            if (txtProductPriceSum.Text.Length == 0 || double.Parse(txtProductPriceSum.Text) == 0)
            {
                MyMessage.Show("Mahsulot narxini so'mda kiriting!");
                return;
            }
            if (txtProductPricePersentage.Text.Length == 0 || double.Parse(txtProductPricePersentage.Text) == 0)
            {
                MyMessage.Show("Mahsulot narxini foizda kiriting!");
                return;
            }
            if (comboMeasurement.SelectedIndex == -1)
            {
                MyMessage.Show("Mahsulot o'lchov birligini tanlang!");
                return;
            }
            if (comboDelivery.SelectedValue == null || (int)comboDelivery.SelectedValue == 0)
            {
                MyMessage.Show("Yetkazib beruvchini tanlang!");
                return;
            }
            if (txtNotePrice.Text.Length == 0 || double.Parse(txtNotePrice.Text) == 0)
            {
                MyMessage.Show("Elsatma narxini foizda kiriting!");
                return;
            }


            ProductForCreationDto creationDto = new ProductForCreationDto()
            {
                BarCode = txtBarCode.Text,
                Name = txtProductName.Text,
                CategoryId = (int)comboCategory.SelectedValue,
                UserId = (int)comboDelivery.SelectedValue,
                CamePrice = decimal.Parse(txtPrice.Text),
                SalePrice = decimal.Parse(txtProductPriceSum.Text),
                PercentageOfPrice = decimal.Parse(txtProductPricePersentage.Text),
                Quantity = decimal.Parse(txtQuantity.Text),
                OlchovTuri = (OlchovBirligi)comboMeasurement.SelectedIndex,
                Action = true,
                //ImagePath = 

            };

            var dbProduct = await _productServise.CreateAsync();
            
        }

        private void btnClear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtBarCode.Text = "";
            txtPCode.Text = "";
            txtProductName.Text = "";
            comboCategory.SelectedValue = 0;
            txtQuantity.Text = "";
            txtPrice.Text= string.Empty;
            txtProductPricePersentage.Text= string.Empty;
            txtProductPriceSum.Text= string.Empty;
            comboMeasurement.SelectedIndex = -1;
            comboDelivery.SelectedValue = 0;
            txtNotePrice.Text= string.Empty;
            lbImage.Content = string.Empty;
        }

        private async void btnCreateCategory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CategoryCreateWindow categoryCreateWindow = new CategoryCreateWindow();
            lab_fon2.Visibility = Visibility.Visible;
            categoryCreateWindow.ShowDialog();
            lab_fon2.Visibility = Visibility.Collapsed;
            await GetAllCategories();

        }

        
    }
}
