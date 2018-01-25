using AdoBankingSystem.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.UnitTests.Services
{
    [TestClass]
    public class RabbitMqConnectionTest
    {
        [TestMethod]
        public void IsConnectionAvailable()
        {
            RabbitMqBusService rbq = new RabbitMqBusService();
            var connection = rbq.IsConnectionAvailable();

        }


    }
}
