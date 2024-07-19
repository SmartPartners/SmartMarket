using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SmartMarket.Desktop.Windows.CustomWindows
{
    /// <summary>
    /// Логика взаимодействия для MyMessage.xaml
    /// </summary>
    public partial class MyMessage : Window
    {
        public static MyMessage _msgBox;
        public static Window parentWindow;
        public static MessageBoxResult result;

        public MyMessage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static void Show(string message, Window parent = null, bool link = false)
        {
            _msgBox = new MyMessage();

            _msgBox.text_message.Text = message;

            if (parent != null)
            {
                _msgBox.Owner = parent;
                parentWindow = parent;

            }
            else
            {
                _msgBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            if (link == true)
            {

                _msgBox.link_message.Visibility = Visibility.Visible;
            }

            _msgBox.okBtnPanel.Visibility = Visibility.Visible;
            _msgBox.YesNoBtnPanel.Visibility = Visibility.Collapsed;

            _msgBox.butt_ok.Focus();

            _msgBox.ShowDialog();
        }

        public static MessageBoxResult Show(string message, string title, Window parent = null)
        {
            _msgBox = new MyMessage();

            _msgBox.text_message.Text = message;
            _msgBox.txt_title.Text = title;
            result = MessageBoxResult.No;

            if (parent != null)
            {
                _msgBox.Owner = parent;
            }
            else
            {
                _msgBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }



            _msgBox.YesNoBtnPanel.Visibility = Visibility.Visible;
            _msgBox.okBtnPanel.Visibility = Visibility.Collapsed;

            _msgBox.butt_yes.Focus();

            _msgBox.ShowDialog();

            _msgBox.butt_yes.Focus();

            return result;
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Yes;

            this.Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.No;

            this.Close();

            Keyboard.ClearFocus();
        }

        private void linkLabel_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;
            Process.Start(new ProcessStartInfo { FileName = link.NavigateUri.AbsoluteUri, UseShellExecute = true });
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            result = MessageBoxResult.Cancel;
            this.Close();
            Keyboard.ClearFocus();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
