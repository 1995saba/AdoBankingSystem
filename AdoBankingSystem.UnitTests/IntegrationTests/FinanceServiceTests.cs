using AdoBankingSystem.BLL.Services;
using AdoBankingSystem.DAL.DAOs;
using AdoBankingSystem.Shared.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.UnitTests.IntegrationTests
{
    [TestClass]
    public class FinanceServiceTests
    {
        [TestMethod]
        public void FinanceService_MakeTransaction_Integration()
        {
            ApplicationClientService appClient = new ApplicationClientService();
            FinanceService financeService = new FinanceService();

            string firstUserId = appClient.RegisterNewBankClient(new BankClientDto()
            {
                FirstName = "Serik",
                LastName = "Serik",
                Email = "serik@gmail.com",
                PasswordHash = "12345",
                ApplicationClientType = ApplicationClientType.BankClient,
            });

            string secondUserId = appClient.RegisterNewBankClient(new BankClientDto()
            {
                FirstName = "Alika",
                LastName = "Alika",
                Email = "alika@gmail.com",
                PasswordHash = "12345",
                ApplicationClientType = ApplicationClientType.BankClient,
            });

            // S = -100, A = +100
            financeService.MakeClientToClientTransaction(firstUserId, secondUserId, 100M);

            // S = -150, A = +150
            financeService.MakeClientToClientTransaction(firstUserId, secondUserId, 50M);

            // S = -110 A = +110
            financeService.MakeClientToClientTransaction(secondUserId, firstUserId, 40M);

            
            var resultForUserOne = financeService.GetHistoryOverview(firstUserId);
            var resultForUserTwo = financeService.GetHistoryOverview(secondUserId);

            Debug.WriteLine(resultForUserOne.Item1 + " " + resultForUserOne.Item2);
            Debug.WriteLine(resultForUserTwo.Item1 + " " + resultForUserTwo.Item2);

            Assert.IsTrue(resultForUserOne.Item1 == 150M && resultForUserOne.Item2 == 40M);
            Assert.IsTrue(resultForUserTwo.Item1 == 40M && resultForUserTwo.Item2 == 150M);


        }
    }
}
