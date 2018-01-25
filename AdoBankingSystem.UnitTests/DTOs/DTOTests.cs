using AdoBankingSystem.Shared.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.UnitTests.DTOs
{
    [TestClass]
    public class DTOTests
    {
        [TestMethod]
        public void EntityDto_GetDetailedTypeInfo()
        {
            BankClientDto bankClientDto =
               new BankClientDto("Assel", "Assel", "Assel", "Assel", "Assel");

            bankClientDto.Id = "2222";

            Debug.WriteLine(bankClientDto.ToString());
        }
    }
}
