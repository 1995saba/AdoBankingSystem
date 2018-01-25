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
    public class CurrentSessionDao : IDAO<CurrentSessionDto>, IExtendedCurrentSession
    {
        private SqlConnection sqlConnection = null;
        public string Create(CurrentSessionDto record)
        {
            record.Id = Guid.NewGuid().ToString();
            using(sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "INSERT INTO [dbo].[CurrentSessions] " +
                    "([Id], [UserId], [SignInTime], [LastOperationTime], [CreatedTime], [EntityStatus])" +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', {5})";

                string realQuery = String.Format
                    (baseQuery, record.Id, record.UserId, record.SignInTime, 
                    record.LastOperationTime, record.CreatedTime, (int)record.EntityStatus);

                sqlConnection.Open();

                using(SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            return record.Id;
        }

        public string GetCurrentSessionIdByUserId(string userId)
        {
            string idToReturn = null;
            using (SqlConnection sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "SELECT * FROM dbo.CurrentSessions WHERE UserId = '{0}'";
                string realQuery = String.Format(baseQuery, userId);
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = realQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        idToReturn = reader["Id"].ToString();
                    }             
                    sqlConnection.Close();
                }
            }
            return idToReturn;
        }
        public CurrentSessionDto Read(string id)
        {
            CurrentSessionDto currentSessionDto;
            using (sqlConnection = new SqlConnection())
            {
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.VarChar);
                idParameter.Value = id;
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "ReadCurrentSession";
                    sqlCommand.Parameters.Add(idParameter);

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Read();
                    currentSessionDto = new CurrentSessionDto()
                    {
                        Id = sqlDataReader["Id"].ToString(),
                        UserId = sqlDataReader["UserId"].ToString(),
                        LastOperationTime = DateTime.Parse(sqlDataReader["LastOperationTime"].ToString()),
                        SignInTime = DateTime.Parse(sqlDataReader["SignInTime"].ToString()),
                        CreatedTime = DateTime.Parse(sqlDataReader["CreatedTime"].ToString()),
                        EntityStatus = (EntityStatusType)Int32.Parse(sqlDataReader["EntityStatus"].ToString())
                    };
                }
                sqlConnection.Close();
            }
            return currentSessionDto;
        }

        public ICollection<CurrentSessionDto> Read()
        {
            ICollection<CurrentSessionDto> sessions = new List<CurrentSessionDto>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string realQuery = "SELECT * FROM dbo.CurrentSessions";              

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        sessions.Add(new CurrentSessionDto()
                        {
                            Id = reader["Id"].ToString(),
                            UserId = reader["UserId"].ToString(),
                            SignInTime = DateTime.Parse(reader["SignInTime"].ToString()),
                            LastOperationTime = DateTime.Parse(reader["LastOperationTime"].ToString()),
                            CreatedTime = DateTime.Parse(reader["CreatedTime"].ToString()),
                            EntityStatus = (EntityStatusType)Int32.Parse(reader["EntityStatus"].ToString())
                        });
                    }
                }
                sqlConnection.Close();
            }
            return sessions;
        }

        public void Remove(string id)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "DELETE FROM dbo.CurrentSessions WHERE Id='{0}'";
                string realQuery = String.Format(baseQuery, id);

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    sqlCommand.ExecuteNonQuery();     
                }
                sqlConnection.Close();
            }
        }

        public string Update(CurrentSessionDto record)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.VarChar);
                SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.VarChar);
                SqlParameter signInTimeParameter = new SqlParameter("@SignInTime", SqlDbType.DateTime);
                SqlParameter lastOperationTimeParameter = new SqlParameter("@LastOperationTime", SqlDbType.DateTime);
                SqlParameter createdTimeParameter = new SqlParameter("@CreatedTime", SqlDbType.DateTime);
                SqlParameter entityStatusParameter = new SqlParameter("@EntityStatus", SqlDbType.Int);

                idParameter.Value = record.Id;
                userIdParameter.Value = record.UserId;
                signInTimeParameter.Value = record.SignInTime;
                lastOperationTimeParameter.Value = record.LastOperationTime;
                createdTimeParameter.Value = record.CreatedTime;
                entityStatusParameter.Value = record.EntityStatus;

                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "UpdateCurrentSession";
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(userIdParameter);
                    sqlCommand.Parameters.Add(signInTimeParameter);
                    sqlCommand.Parameters.Add(lastOperationTimeParameter);
                    sqlCommand.Parameters.Add(createdTimeParameter);
                    sqlCommand.Parameters.Add(entityStatusParameter);

                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            return record.Id.ToString();
        }

        
    }
}
