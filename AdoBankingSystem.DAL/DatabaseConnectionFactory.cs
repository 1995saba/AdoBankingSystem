using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL
{
    public class DatabaseConnectionFactory
    {
        private static string ConnectionString =>
            ConfigurationManager
                .ConnectionStrings["PrimaryConnectionString"]
                .ToString();

        // TO DO: Add Applicatio Name info
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
