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
    public class TransactionDao
    {
        string connectionString = ConfigurationManager
                   .ConnectionStrings["PrimaryConnectionString"]
                   .ToString();

        string sql = @"SELECT * FROM Transactions";

        public void Create(TransactionDto dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Transactions");

                    DataRow dataRowToAdd = dataSet.Tables["Transactions"].NewRow();

                    foreach (var item in dto.GetType().GetProperties())
                    {
                        dataRowToAdd[item.Name] = item.GetValue(dto, null);
                    }

                    dataSet.Tables["Transactions"].Rows.Add(dataRowToAdd);

                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet.Tables["Transactions"]);
                }
                sqlConnection.Close();
            }
        }

        public TransactionDto Read(string id)
        {
            TransactionDto dtoToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Transactions");
                    dataSet.Tables["Transactions"].PrimaryKey = new DataColumn[] { dataSet.Tables["Transactions"].Columns["Id"] };

                    DataRow dataRowToReturn = dataSet.Tables["Transactions"].Rows.Find(id);

                    foreach (var item in dataRowToReturn.ItemArray.ToList())
                    {
                        Console.WriteLine(item);
                    }
                }
                sqlConnection.Close();
            }
            return null;
        }

        public void Update(TransactionDto dto)
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
                    key[0] = dataSet.Tables["Transactions"].Columns[0];
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
