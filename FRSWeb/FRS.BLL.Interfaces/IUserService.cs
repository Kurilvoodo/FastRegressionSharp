using FRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
