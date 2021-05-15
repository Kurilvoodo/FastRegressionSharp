using FRS.Entities;

namespace FRS.BLL.Interfaces
{
    public interface IUserService
    {
        int AddUser(User user);

        void DeleteUserById(int userId);

        bool Login(User user);

        int GetUserIdByUsername(string Username);

        User GetUserById(int id);
    }
}