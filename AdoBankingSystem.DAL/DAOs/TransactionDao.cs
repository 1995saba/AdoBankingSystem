using AdoBankingSystem.DAL.Interfaces;
using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL.DAOs
{
    public class TransactionDao : IDAO<TransactionDto>
    {
        private SqlConnection sqlConnection = null;
        public string Create(TransactionDto record)
        {
            record.Id = Guid.NewGuid().ToString();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "INSERT INTO [dbo].[Transactions]" +
                    "([Id],[SenderId],[ReceiverId],[TransactionType],[TransactionAmount],[TransactionTime], [CreatedTime], [EntityStatus])" +
                    "VALUES ('{0}','{1}','{2}','{3}',{4}, '{5}', '{6}', {7})";

                string realQuery = String.Format
                    (baseQuery, record.Id, record.SenderId, record.ReceiverId,
                    (int)record.TransactionType, record.TransactionAmount, record.TransactionTime, record.CreatedTime, (int)record.EntityStatus);

                sqlConnection.Open();

                using (SqlCommand sqlCommand=sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            return record.Id;
        }

        public TransactionDto Read(string id)
        {
            TransactionDto transactionDtoReturn = null;
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string realQuery = "SELECT * FROM dbo.Transactions where Id=" + id.ToString();

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader != null)
                        transactionDtoReturn = new TransactionDto()
                        {
                            SenderId = reader["SenderId"].ToString(),
                            ReceiverId = reader["ReceiverId"].ToString(),
                            TransactionType = (TransactionType)Int32.Parse(reader["TransactionType"].ToString()),
                            TransactionAmount = Decimal.Parse(reader["TransactionAmount"].ToString()),
                            TransactionTime = DateTime.Parse(reader["TransactionTime"].ToString()),
                        };
                }
            }
            sqlConnection.Close();

            return transactionDtoReturn;

        }

        public ICollection<TransactionDto> Read()
        {
            ICollection<TransactionDto> transactionDtoReturn = new List<TransactionDto>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string realQuery = "SELECT * FROM dbo.Transactions";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        transactionDtoReturn.Add(new TransactionDto()
                        {
                            SenderId = reader["SenderId"].ToString(),
                            ReceiverId = reader["ReceiverId"].ToString(),
                            TransactionType = (TransactionType)Int32.Parse(reader["TransactionType"].ToString()),
                            TransactionAmount = Decimal.Parse(reader["TransactionAmount"].ToString()),
                            TransactionTime = DateTime.Parse(reader["TransactionTime"].ToString()),
                            EntityStatus = (EntityStatusType)Int32.Parse(reader["EntityStatus"].ToString()),
                            CreatedTime = DateTime.Parse(reader["CreatedTime"].ToString()),
                            Id = reader["Id"].ToString()
                        });
                    }
                }
                sqlConnection.Close();
            }
            return transactionDtoReturn;
        }

        public ICollection<TransactionDto> ReadAllTransactionsOfUser(string userId)
        {
            ICollection<TransactionDto> transactionDtoReturn = new List<TransactionDto>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "SELECT * FROM dbo.Transactions WHERE SenderId = '{0}' OR ReceiverId = '{0}'";
                string realQuery = String.Format(baseQuery, userId);
                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        transactionDtoReturn.Add(new TransactionDto()
                        {
                            SenderId = reader["SenderId"].ToString(),
                            ReceiverId = reader["ReceiverId"].ToString(),
                            TransactionType = (TransactionType)Int32.Parse(reader["TransactionType"].ToString()),
                            TransactionAmount = Decimal.Parse(reader["TransactionAmount"].ToString()),
                            TransactionTime = DateTime.Parse(reader["TransactionTime"].ToString()),
                            EntityStatus = (EntityStatusType)Int32.Parse(reader["EntityStatus"].ToString()),
                            CreatedTime = DateTime.Parse(reader["CreatedTime"].ToString()),
                            Id = reader["Id"].ToString()
                        });
                    }
                }
                sqlConnection.Close();
            }
            return transactionDtoReturn;
        }
        public void Remove(string id)
        {
            string baseQuery = "DELETE FROM [dbo].[Transactions] WHERE Id = " + id;
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(baseQuery, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public string Update(TransactionDto record)
        {
            string baseQuery = "UPDATE dbo.Transactions SET Id = '{0}',SenderId = '{1}',ReceiverId = '{2}',TransactionType = {3},TransactionAmount = {4},TransactionTime = '{5}',CreatedTime = '{6}',EntityStatus = {7}";
            string realQuery = String.Format(baseQuery, record.Id, record.SenderId,
                record.ReceiverId, record.TransactionType, record.TransactionAmount,
                record.TransactionTime, record.CreatedTime, record.EntityStatus);
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(realQuery, sqlConnection))
                {
                    sqlConnection.Close();
                    return command.ExecuteNonQuery().ToString();
                }

            }
        }
    }
}
