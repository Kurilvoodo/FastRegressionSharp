using FRS.DAL.Interfaces;
using FRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.DAO
{
    public class UserDao : IUserDao
    {
        public int AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetUserIdByUsername(string Username)
        {
            throw new NotImplementedException();
        }

        public bool Login(User user)
        {
            throw new NotImplementedException();
        }
    }
}
