using FRS.Entities;

namespace FRS.BLL.Interfaces
{
    public interface IUserService
    {
        int AddUser(User user);

        void DeleteUserById(int userId);

        bool Login(User user);

        int GetUserIdByUsername(string username);

        User GetUserById(int id);

        SubInfo GetSubInfo(int userId);

        void AddSubInfo(int userId);

        int ApiAuth(string accessKey, string secretAccessKey);
    }
}