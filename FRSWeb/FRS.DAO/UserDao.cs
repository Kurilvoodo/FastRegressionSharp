using FRS.DAL.Interfaces;
using FRS.Entities;
using System.Data;
using System.Data.SqlClient;

namespace FRS.DAO
{
    public class UserDao : BaseDao, IUserDao
    {
        public void AddSubInfo(SubInfo subInfo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.AddSubInfo");
                AddParameter(GetParameter("@UserId", subInfo.UserId, DbType.Int32), command);
                AddParameter(GetParameter("@AccessKey", subInfo.AccessKey, DbType.String), command);
                AddParameter(GetParameter("@SecretAccessKey", subInfo.SecretAccessKey, DbType.String), command);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.AddUser");
                AddParameter(GetParameter("@Username", user.Username, DbType.String), command);
                AddParameter(GetParameter("@Password", user.HashPassword, DbType.Binary), command);
                connection.Open();
                var userId = command.ExecuteScalar();
                return (int)userId;
            }
        }

        public int ApiAuth(string accessKey, string secretAccessKey)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.ApiAuth");
                AddParameter(GetParameter("@AccessKey", accessKey, DbType.String), command);
                AddParameter(GetParameter("@SecretAccessKey", secretAccessKey, DbType.String), command);
                connection.Open();
                var reader = command.ExecuteReader();
                int userId = 0;
                while (reader.Read())
                {
                    userId = (int)reader["UserId"];
                }
                return userId;
            }
        }

        public void DeleteUserById(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.RemoveUser");
                AddParameter(GetParameter("@ID", userId, DbType.Int32), command);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public SubInfo GetSubInfo(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.GetSubInfo");
                AddParameter(GetParameter("@UserId", userId, DbType.Int32), command);
                connection.Open();
                var reader = command.ExecuteReader();
                SubInfo subInfo = null;
                while (reader.Read())
                {
                    subInfo = new SubInfo()
                    {
                        UserId = (int)reader["UserId"],
                        AccessKey = reader["AccessKey"] as string,
                        SecretAccessKey = reader["SecretAccessKey"] as string
                    };
                }
                return subInfo;
            }
        }

        public User GetUserById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.GetUserById");
                AddParameter(GetParameter("@ID", id, DbType.Int32), command);
                connection.Open();
                User userinfo = null;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userinfo = new User()
                    {
                        Username = reader["Username"] as string
                    };
                }
                return userinfo;
            }
        }

        public int GetUserIdByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.GetUserByUsername");
                AddParameter(GetParameter("@Username", username, DbType.String), command);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public bool Login(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.Authentication");
                AddParameter(GetParameter("@Username", user.Username, DbType.String), command);
                AddParameter(GetParameter("@Password", user.HashPassword, DbType.Binary), command);
                connection.Open();
                var resultCommand = command.ExecuteScalar();
                return (int)resultCommand > 0;
            }
        }
    }
}