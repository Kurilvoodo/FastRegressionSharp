using FRS.BLL.Interfaces;
using FRS.DAL.Interfaces;
using FRS.Entities;
using System;


namespace FRS.BLL.Services
{
    public class UserService : IUserService
    {
        private IUserDao _userDao;
        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }
        public int AddUser(User user)
        {
            return _userDao.AddUser(user);
        }

        public void DeleteUserById(int userId)
        {
            _userDao.DeleteUserById(userId);
        }

        public User GetUserById(int userId)
        {
            return _userDao.GetUserById(userId);
        }

        public int GetUserIdByUsername(string username)
        {
            return _userDao.GetUserIdByUsername(username);
        }

        public bool Login(User user)
        {
            return _userDao.Login(user);
        }
    }
}
