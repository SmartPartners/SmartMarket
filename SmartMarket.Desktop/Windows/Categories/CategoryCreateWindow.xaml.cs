using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.Service.Categories;
using SmartMarket.Service.DTOs.Categories;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static SmartMarket.Desktop.Windows.BlurEffects.BlurEffect;

namespace SmartMarket.Desktop.Windows.Categories
{
    /// <summary>
    /// Interaction logic for CategoryCreateWindow.xaml
    /// </summary>
    public partial class CategoryCreateWindow : Window
    {
        private ICategoriesService _categoryService;
        public CategoryCreateWindow()
        {
            InitializeComponent();
            this._categoryService = new CategoriesService();
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private async void Border_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           this.Close();
        }

        private async void btnCategoryCreate_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("Kategoriyaning nomini kiriting!");
                return;
            }

            CategoryForCreationDto category = new CategoryForCreationDto()
            {
                Name = txtName.Text,
            };

            var dbresult = await _categoryService.CreateAsync(category);
            if (dbresult != null)
            {
                this.Close();

            }
            else MessageBox.Show("Yaratishda xato !");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //EnableBlur();
        }
    }
}
