using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
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

namespace SmartMarket.Desktop.Windows.AccountSettings
{
    /// <summary>
    /// Interaction logic for AccountSettingCreateWindow.xaml
    /// </summary>
    public partial class AccountSettingCreateWindow : Window
    {
        public AccountSettingCreateWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            if (!IsRunAsAdministrator())
            {
                // Restart the application with admin rights
                var processInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = System.Reflection.Assembly.GetExecutingAssembly().Location,
                    Verb = "runas"
                };

                try
                {
                    Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("This application must be run as Administrator. " + ex.Message);
                }

                Application.Current.Shutdown();
            }
            else
            {
                // Your code to start osk.exe
                StartOnScreenKeyboard();
            }
        }

        private bool IsRunAsAdministrator()
        {
            var wi = WindowsIdentity.GetCurrent();
            var wp = new WindowsPrincipal(wi);
            return wp.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void StartOnScreenKeyboard()
        {
            try
            {
                Process.Start("osk.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start osk.exe: " + ex.Message);
            }
        }
    }
}
