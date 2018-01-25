using AdoBankingSystem.BLL.Services;
using AdoBankingSystem.Shared.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.UnitTests.Services
{
    [TestClass]
    public class ApplicationClientServiceTests
    {
        [TestMethod]
        public void ApplicationClientService_RegisterNewBankClient()
        {
            ApplicationClientService applicationClientService = new ApplicationClientService();

            BankClientDto bankClientDto =
                new BankClientDto("Test", "Test", "Test", "Test", "Test");

            applicationClientService.RegisterNewBankClient(bankClientDto);

            string result = bankClientDto.ToString();
            Debug.WriteLine(result);
        }

        [TestMethod]
        public void ApplicationClientService_SignInToApplication()
        {
            ApplicationClientService applicationClientService = new ApplicationClientService();
            applicationClientService.SignInToApplication("Test", "Test");

        }
    }
}
