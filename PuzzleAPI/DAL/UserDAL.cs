using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleAPI.DAL
{
    public class UserDAL : IUsers
    {
        //Creating DBContext object for EF Core
        private DBContext _context;
        public UserDAL(DBContext context)
        {
            _context = context;
        }

        // returns all users
        public List<User> GetUsers()
        {
            List<User> result;
            try
            {
                result = _context.users.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            if (result.Count > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        // returns sepcified user
        public User GetUser(int userId)
        {
            User result;
            try
            {
                result = _context.users.Where(p => p.userId == userId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        // creates new user
        public void AddUser(string appId)
        {
            User newUser = new User { appId = appId, points = 0 };
            try
            {
                _context.users.Add(newUser);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // deletes specified user
        public void DeleteUser(int userId)
        {
            User result;
            try
            {
                result = _context.users.Where(p => p.userId == userId).FirstOrDefault();
                if (result != null)
                {
                    List<UserQuestions> userQs = _context.userQuestions.Where(p => p.userId == userId).ToList();
                    _context.users.Remove(result);
                    foreach (UserQuestions question in userQs)
                    {
                        _context.userQuestions.Remove(question);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // updates specified user
        public void UpdateUser(User user)
        {
            User result;
            try
            {
                result = _context.users.Where(p => p.userId == user.userId).FirstOrDefault();
                if (result != null)
                {
                    result.appId = user.appId;
                    result.points = user.points;
                    _context.users.Update(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
