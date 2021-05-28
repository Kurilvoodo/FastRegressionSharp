using FRS.Entities;

namespace FRS.DAL.Interfaces
{
    public interface IUserDao
    {
        int AddUser(User user);

        void DeleteUserById(int userId);

        bool Login(User user);

        int GetUserIdByUsername(string Username);

        User GetUserById(int id);

        void AddSubInfo(SubInfo subInfo);

        SubInfo GetSubInfo(int userId);

        int ApiAuth(string accessKey, string secretAccessKey);
    }
}