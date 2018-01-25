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
    public class BankManagerDao : IDAO<BankManagerDto>
    {
        private SqlConnection sqlConnection;

        public string Create(BankManagerDto record)
        {
            NewMethod(record, "CreateNewBankManager");
            return record.Id;
        }

        public BankManagerDto Read(string id)
        {
            BankManagerDto bankManagerDto;
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.VarChar);
                idParameter.Value = id;
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "ReadBankManager";
                    sqlCommand.Parameters.Add(idParameter);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Read();
                    bankManagerDto = new BankManagerDto()
                    {
                        Id = sqlDataReader["Id"].ToString(),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        LastName = sqlDataReader["LastName"].ToString(),
                        Email = sqlDataReader["Email"].ToString(),
                        PasswordHash = sqlDataReader["PasswordHash"].ToString(),
                        CreatedTime = DateTime.Parse(sqlDataReader["CreatedTime"].ToString()),
                        EntityStatus = (EntityStatusType)Int32.Parse(sqlDataReader["EntityStatus"].ToString())
                    };
                }
                sqlConnection.Close();
            }
            return bankManagerDto;
        }

        public ICollection<BankManagerDto> Read()
        {
            ICollection<BankManagerDto> managersToReturn = new List<BankManagerDto>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string realQuery = "SELECT * FROM dbo.BankManagers";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        managersToReturn.Add(new BankManagerDto()
                        {
                            Id = reader["Id"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            CreatedTime = DateTime.Parse(reader["CreatedTime"].ToString()),
                            EntityStatus = (EntityStatusType)Int32.Parse(reader["EntityStatus"].ToString())
                        });
                    }
                }
                sqlConnection.Close();
            }
            return managersToReturn;
        }

        public void Remove(string id)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "DELETE FROM dbo.Managers WHERE Id = " + id;
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public string Update(BankManagerDto record)
        {
            NewMethod(record, "UpdateBankManager");
            return record.Id;
        }

        private void NewMethod(BankManagerDto record, string sqlCommandText)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.VarChar);
                SqlParameter firstNameParameter = new SqlParameter("@FirstName", SqlDbType.VarChar);
                SqlParameter lastNameParameter = new SqlParameter("@LastName", SqlDbType.VarChar);
                SqlParameter emailParameter = new SqlParameter("@Email", SqlDbType.VarChar);
                SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarChar);
                SqlParameter createdTimeParameter = new SqlParameter("@CreatedTime", SqlDbType.DateTime);
                SqlParameter entityStatusParameter = new SqlParameter("@EntityStatus", SqlDbType.Int);

                idParameter.Value = record.Id;
                firstNameParameter.Value = record.FirstName;
                lastNameParameter.Value = record.LastName;
                emailParameter.Value = record.Email;
                passwordHashParameter.Value = record.PasswordHash;
                createdTimeParameter.Value = record.CreatedTime;
                entityStatusParameter.Value = record.EntityStatus;

                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = sqlCommandText;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(firstNameParameter);
                    sqlCommand.Parameters.Add(lastNameParameter);
                    sqlCommand.Parameters.Add(emailParameter);
                    sqlCommand.Parameters.Add(passwordHashParameter);
                    sqlCommand.Parameters.Add(createdTimeParameter);
                    sqlCommand.Parameters.Add(entityStatusParameter);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }
    }
}
