using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.Utils
{
    public static class ConnectionManagerUtil
    {
        private readonly static IConnectionFactory _connectionFactory = null;
        public static bool IsConnectionAvailable()
        {
            try
            {
                IConnection connection = GetConnection();
                return connection.IsOpen;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static IConnection GetConnection()
        {
            IConnectionFactory connectionFactory = new ConnectionFactory()
            {
                UserName = "test",
                Password = "test",
                VirtualHost = "/",
                Port = 5672,
                HostName = "52.166.219.193"
            };
            return connectionFactory.CreateConnection();
        }
    }
}
