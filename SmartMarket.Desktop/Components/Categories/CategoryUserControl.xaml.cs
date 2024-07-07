using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.Service.Categories;
using SmartMarket.Desktop.Windows.Categories;
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

        public CategoryUserControl()
        {
            InitializeComponent();
            this._categoryService = new CategoriesService();
        }

        public void SetaData(int i, Category category)
        {
            this.category = category;
            lbNumber.Text = i.ToString();
            lbName.Text = category.Name;
        }

        private async void btnDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Rostdan ham bu Kategoriyani o'chirmoqchimisiz ?", "O'chirish", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dbResult = await _categoryService.DeleteAsync(category.Id);
                if (dbResult == true)
                {
                    await Refresh();                    
                }
                else MessageBox.Show("O'chirishda xato !");
            }
        }

        private async void btnUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CategoryUpdateWindow updateWindow = new CategoryUpdateWindow();
            updateWindow.SetaData(category);
            updateWindow.ShowDialog();
            await Refresh();
        }
    }
}
