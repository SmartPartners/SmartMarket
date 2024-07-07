using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.Service.Categories;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Service.DTOs.Categories;
using System.Windows;
using System.Windows.Input;

namespace SmartMarket.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for CategoryUpdateWindow.xaml
    /// </summary>
    public partial class CategoryUpdateWindow : Window
    {
        private Category category;
        private ICategoriesService _categoryService;

        public CategoryUpdateWindow()
        {
            InitializeComponent();
            this._categoryService = new CategoriesService();

        }

        public void SetaData(Category category)
        {
            this.category = category;
            txtName.Text = category.Name;

        }
        private async void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Kategoriyaning nomini kiriting!");
                return;
            }

            CategoryForCreationDto creationDto = new CategoryForCreationDto()
            {
                Name = txtName.Text,
            };

            //var dbresult = await _categoryService.UpdateAsync(category.Id,creationDto);
            //if (dbresult != null)
            //{
            //    this.Close();
            //}
            //else MessageBox.Show("Tahrirlashda xato!");
        }
    }
}
