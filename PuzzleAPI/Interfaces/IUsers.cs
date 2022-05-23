using PuzzleAPI.Models;
using System.Collections.Generic;

namespace PuzzleAPI.Interfaces
{
    public interface IUsers
    {
        // gets all users
        List<User> GetUsers();
        // gets specified user
        User GetUser(int userId);
        // creates new user
        void AddUser(string appId);
        // deletes specified user
        void DeleteUser(int userId);
        // updates user
        void UpdateUser(User user);
    }
}
