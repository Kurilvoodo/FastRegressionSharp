using FRS.DAL.Interfaces;
using FRS.Entities;
using System.Data;
using System.Data.SqlClient;

namespace FRS.DAO
{
    public class UserDao : BaseDao, IUserDao
    {
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
                SqlCommand command = GetCommand(connection, "dbo.GetIdByUsername");
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