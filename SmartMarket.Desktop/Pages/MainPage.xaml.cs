﻿using SmartMarket.Desktop.Components.Categories;
using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.Interfrace.ContrAgents;
using SmartMarket.Desktop.Service.Interfrace.Product;
using SmartMarket.Desktop.Service.Service.Categories;
using SmartMarket.Desktop.Service.Service.ContrAgents;
using SmartMarket.Desktop.Service.Service.Products;
using SmartMarket.Desktop.Windows.Categories;
using SmartMarket.Desktop.Windows.ContrAgents;
using SmartMarket.Desktop.Windows.Products;
using SmartMarket.Domin.Entities.Categories;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private ICategoriesService _categoryService;
        private IProductService _productService;
        private IContrAgentService _contrAgentService;
        public MainWindow MainWindowParent;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this._categoryService = new CategoriesService();
            this._productService = new ProductService();
            this._contrAgentService = new ContrAgentService();
            this.MainWindowParent = mainWindow;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RefreshCategories();
            await RefreshProducts();
            await RefreshKontrAgents();
        }

        public async Task RefreshProducts()
        {
            var dbProducts = await _productService.GetProductsAsync();

            if (dbProducts != null)
            {
                grProducts.ItemsSource = dbProducts;
            }
        }

        public async Task RefreshKontrAgents()
        {
            var dbContrAgents = await _contrAgentService.GetContrAgentsAsync();

            if (dbContrAgents != null)
            {
                grContrAgent.ItemsSource = dbContrAgents;
            }
        }

        public async Task RefreshCategories()
        {
            stCategory.Children.Clear();

            var dbCategories = await _categoryService.GetcategoriesAsync();

            if (dbCategories != null)
            {
                int i = 0;
                foreach (var category in dbCategories)
                {
                    i++;
                    CategoryUserControl userControl = new CategoryUserControl(this);
                    userControl.SetaData(i, category);
                    userControl.Refresh = RefreshCategories;
                    stCategory.Children.Add(userControl);
                }
            }
        }

        private async void btnCategoryCreate_Click(object sender, RoutedEventArgs e)
        {
            MainWindowParent.lab_fon.Visibility = Visibility.Visible;
            CategoryCreateWindow categoryWindow = new CategoryCreateWindow();
            categoryWindow.ShowDialog();
            MainWindowParent.lab_fon.Visibility = Visibility.Collapsed;
            await RefreshCategories();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowParent.lab_fon.Visibility = Visibility.Visible;
            ContrAgentsCreateWindow createWindow = new ContrAgentsCreateWindow();
            createWindow.ShowDialog();
            MainWindowParent.lab_fon.Visibility = Visibility.Collapsed;
            await RefreshKontrAgents();
        }

        private void btnProductCreate_Click(object sender, RoutedEventArgs e)
        {
            MainWindowParent.lab_fon.Visibility = Visibility.Visible;
            ProductCreateWindow productCreateWindow = new ProductCreateWindow();
            productCreateWindow.ShowDialog();
            MainWindowParent.lab_fon.Visibility = Visibility.Collapsed;
        }
    }
}
