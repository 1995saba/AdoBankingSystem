using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL.SQLAdapterDAOs
{
    public class BankManagerDao
    {
        string connectionString = ConfigurationManager
                       .ConnectionStrings["PrimaryConnectionString"]
                       .ToString();

        string sql = @"SELECT * FROM BankManagers";

        public void Create(BankManagerDto dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "BankManagers");

                    DataRow dataRowToAdd = dataSet.Tables["BankManagers"].NewRow();

                    foreach (var item in dto.GetType().GetProperties())
                    {
                        dataRowToAdd[item.Name] = item.GetValue(dto, null);
                    }

                    dataSet.Tables["BankManagers"].Rows.Add(dataRowToAdd);

                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet.Tables["BankManagers"]);
                }
                sqlConnection.Close();
            }
        }

        public BankManagerDto Read(string id)
        {
            BankManagerDto dtoToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "BankManagers");
                    dataSet.Tables["BankManagers"].PrimaryKey = new DataColumn[] { dataSet.Tables["BankManagers"].Columns["Id"] };

                    DataRow dataRowToReturn = dataSet.Tables["BankManagers"].Rows.Find(id);

                    foreach (var item in dataRowToReturn.ItemArray.ToList())
                    {
                        Console.WriteLine(item);
                    }
                }
                sqlConnection.Close();
            }
            return null;
        }

        public void Update(BankManagerDto dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dataSet.Tables[0].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow dataRow = dataSet.Tables[0].Rows.Find(dto.Id);

                    dataRow.BeginEdit();

                    dataRow["Id"] = dto.Id;

                    dataRow.EndEdit();
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }
        public void Delete(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    DataColumn[] key = new DataColumn[1];
                    key[0] = dataSet.Tables["BankManagers"].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow toDelete = dataSet.Tables[0].Rows.Find(id);

                    toDelete.Delete();
                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }
    }
}
