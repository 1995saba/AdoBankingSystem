using AdoBankingSystem.BLL.Interfaces;
using AdoBankingSystem.BLL.Services;
using AdoBankingSystem.ClientUI.Base;
using AdoBankingSystem.Shared.DTOs;
using AdoBankingSystem.Shared.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoBankingSystem.ClientUI.Forms
{
    public partial class RegistrationWindow : Form
    {
        private OfflineModeStorageService _offlineDataPublisher;
        private RabbitMqBusService _onlineDataPublisher;
        private ApplicationClientService _applicationClientService;
        private async void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox2.Text;
            string lastName = textBox1.Text;

            BankClientDto bankClientDto = new BankClientDto()
            {
                FirstName = firstName,
                LastName = lastName,
                ApplicationClientType = ApplicationClientType.BankClient,
                CreatedTime = DateTime.Now,
                Email = firstName,
                EntityStatus = EntityStatusType.IsActive,
                PasswordHash = "12345"
            };

            if (ConnectionManagerUtil.IsConnectionAvailable())
            {
                var pastData = _offlineDataPublisher.GetAllPastData<BankClientDto>();
                if(pastData.Count > 0)
                {
                    var sortedData = pastData.OrderBy(p => p.CreatedTime);
                    foreach (var item in sortedData)
                    {
                        _onlineDataPublisher.PublishMessageToStorage(item);
                    }
                }
                
                _onlineDataPublisher.PublishMessageToStorage(bankClientDto);
            }
            else _offlineDataPublisher.PublishMessageToStorage(bankClientDto);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox8.Text;
            string password = textBox9.Text;

            // Sign In
            bool result = _applicationClientService
                .SignInToApplication(name, password);
            // Go to next Form (Transaction)
            // do it now...

            if (result)
            {
                Debug.WriteLine("Test");
            }
        }

        public RegistrationWindow()
        {
            _onlineDataPublisher = new RabbitMqBusService();
            _offlineDataPublisher = new OfflineModeStorageService();
            _applicationClientService = new ApplicationClientService();
            InitializeComponent();
        }
    }
}
