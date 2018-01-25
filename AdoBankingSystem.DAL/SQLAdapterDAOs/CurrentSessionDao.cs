using AdoBankingSystem.DAL.Interfaces;
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
    public class CurrentSessionDao
    {
        string connectionString = ConfigurationManager
                   .ConnectionStrings["PrimaryConnectionString"]
                   .ToString();

        string sql = @"SELECT * FROM CurrentSessions";

        public void Create(CurrentSessionDto dto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "CurrentSessions");

                    DataRow dataRowToAdd = dataSet.Tables["CurrentSessions"].NewRow();

                    foreach (var item in dto.GetType().GetProperties())
                    {
                        dataRowToAdd[item.Name] = item.GetValue(dto, null);
                    }

                    dataSet.Tables["CurrentSessions"].Rows.Add(dataRowToAdd);

                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(adapter);

                    adapter.Update(dataSet.Tables["CurrentSessions"]);
                }
                sqlConnection.Close();
            }
        }

        public CurrentSessionDto Read(string id)
        {
            CurrentSessionDto dtoToReturn = null;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "CurrentSessions");
                    dataSet.Tables["CurrentSessions"].PrimaryKey = new DataColumn[] { dataSet.Tables["CurrentSessions"].Columns["Id"] };

                    DataRow dataRowToReturn = dataSet.Tables["CurrentSessions"].Rows.Find(id);

                    foreach (var item in dataRowToReturn.ItemArray.ToList())
                    {
                        Console.WriteLine(item);
                    }
                }
                sqlConnection.Close();
            }
            return null;
        }

        public void Update(CurrentSessionDto dto)
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

                    dataRow["UserId"] = dto.UserId;

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
                    key[0] = dataSet.Tables[0].Columns[0];
                    dataSet.Tables[0].PrimaryKey = key;

                    DataRow toDelete = dataSet.Tables["CurrentSessions"].Rows.Find(id);

                    toDelete.Delete();
                    adapter.Update(dataSet);
                }
                sqlConnection.Close();
            }
        }
    }
}
