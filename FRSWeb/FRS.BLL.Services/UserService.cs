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

        public void AddSubInfo(int userId)
        {
            SubInfo subInfo = new SubInfo()
            {
                UserId = userId,
                SecretAccessKey = new Guid().ToString(),
                AccessKey = new Guid().ToString()
            };
            _userDao.AddSubInfo(subInfo);
        }

        public int AddUser(User user)
        {
            return _userDao.AddUser(user);
        }

        public int ApiAuth(string accessKey, string secretAccessKey)
        {
            return _userDao.ApiAuth(accessKey, secretAccessKey);
        }

        public void DeleteUserById(int userId)
        {
            _userDao.DeleteUserById(userId);
        }

        public SubInfo GetSubInfo(int userId)
        {
            return _userDao.GetSubInfo(userId);
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