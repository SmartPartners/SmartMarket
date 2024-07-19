using SmartMarket.Desktop.Pages;
using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.Service.Categories;
using SmartMarket.Desktop.Windows.Categories;
using SmartMarket.Desktop.Windows.CustomWindows;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Service.DTOs.Categories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace SmartMarket.Desktop.Components.Categories
{
    /// <summary>
    /// Interaction logic for CategoryUserControl.xaml
    /// </summary>
    public partial class CategoryUserControl : UserControl
    {
        private ICategoriesService _categoryService;
        private Category category;
        public Func<Task> Refresh;
        MainPage MainPage;

        public CategoryUserControl(MainPage mainPage)
        {
            InitializeComponent();
            this._categoryService = new CategoriesService();
            this.MainPage = mainPage;
        }

        public void SetaData(int i, Category category)
        {
            this.category = category;
            lbNumber.Text = i.ToString();
            lbName.Text = category.Name;
        }


        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MainWindowParent.lab_fon.Visibility = Visibility.Visible;
            CategoryUpdateWindow updateWindow = new CategoryUpdateWindow();
            updateWindow.SetaData(category);
            updateWindow.ShowDialog();
            MainPage.MainWindowParent.lab_fon.Visibility = Visibility.Collapsed;
            await Refresh();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MainWindowParent.lab_fon.Visibility = Visibility.Visible;
            var result = MyMessage.Show("Rostdan ham bu Kategoriyani o'chirmoqchimisiz ?", "O'chirish", MainPage.MainWindowParent);
            MainPage.MainWindowParent.lab_fon.Visibility = Visibility.Collapsed;

            if (result == MessageBoxResult.Yes)
            {
                var dbResult = await _categoryService.DeleteAsync(category.Id);
                if (dbResult == true)
                {
                    await Refresh();
                }
                else
                {
                    MainPage.MainWindowParent.lab_fon.Visibility = Visibility.Visible;
                    MyMessage.Show("O'chirishda xato !", MainPage.MainWindowParent);
                    MainPage.MainWindowParent.lab_fon.Visibility = Visibility.Collapsed;

                }
            }
        }
    }
}
