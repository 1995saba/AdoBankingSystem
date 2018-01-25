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
    public class BankClientDao : IDAO<BankClientDto>
    {
        private SqlConnection sqlConnection = null;

        public string Create(BankClientDto record)
        {
            using(sqlConnection = DatabaseConnectionFactory.GetConnection())
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
                using(SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "CreateNewBankClient";
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(firstNameParameter);
                    sqlCommand.Parameters.Add(lastNameParameter);
                    sqlCommand.Parameters.Add(emailParameter);
                    sqlCommand.Parameters.Add(passwordHashParameter);
                    sqlCommand.Parameters.Add(createdTimeParameter);
                    sqlCommand.Parameters.Add(entityStatusParameter);

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                    return record.Id;
                }
            }
        }

        public BankClientDto Read(string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<BankClientDto> Read()
        {
            ICollection<BankClientDto> users = new List<BankClientDto>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string realQuery = "SELECT * FROM dbo.BankClients";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new BankClientDto()
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
            return users;
        }

        public void Remove(string id)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "DELETE FROM dbo.BankClients WHERE Id = " + id;
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public string Update(BankClientDto record)
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
                    sqlCommand.CommandText = "UpdateBankClient";
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
            return record.Id;
        }
    }
}
