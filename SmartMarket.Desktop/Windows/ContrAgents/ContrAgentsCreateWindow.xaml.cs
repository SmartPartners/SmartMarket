using SmartMarket.Desktop.Service.Interfrace.ContrAgents;
using SmartMarket.Desktop.Service.Service.ContrAgents;
using SmartMarket.Service.DTOs.ContrAgents;
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

namespace SmartMarket.Desktop.Windows.ContrAgents
{
    /// <summary>
    /// Interaction logic for ContrAgentsCreateWindow.xaml
    /// </summary>
    public partial class ContrAgentsCreateWindow : Window
    {
        private IContrAgentService _service;

        public ContrAgentsCreateWindow()
        {
            InitializeComponent();
            this._service = new ContrAgentService();
        }

        private async void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Firma nomini kiriting !");
                return;
            }
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Ism kiriting !");
                return;
            }
            if (txtLastName.Text == "")
            {
                MessageBox.Show("Familiya kiriting !");
                return;
            }
            if (txtPhoneNumber.Text == "")
            {
                MessageBox.Show("Ism kiriting !");
                return;
            }

            ContrAgentForCreationDto creationDto = new ContrAgentForCreationDto()
            {
                Firma = txtName.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,    
                PhoneNumber = txtPhoneNumber.Text,
            };

            var dbCreate = await _service.CreateAsync(creationDto);

            if (dbCreate != null)
            {

            }

        }
    }
}
