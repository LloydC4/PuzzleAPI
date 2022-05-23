using Microsoft.AspNetCore.Mvc;
using PuzzleAPI.Interfaces;
using PuzzleAPI.Models;
using System;

namespace PuzzleAPI.Controllers
{
    [ApiController]
    [Route("api/Puzzle")]
    public class UserController : Controller
    {
        // allows access to the DAL
        IUsers _Service;
        public UserController(IUsers service)
        {
            _Service = service;
        }
        // gets all users
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var result = _Service.GetUsers();
                if (result == null) return NotFound();
                return Ok(result);


            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // gets specified user
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int userId)
        {
            try
            {
                var result = _Service.GetUser(userId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // creates new user
        [HttpPost]
        [Route("AddUser")]
        public void AddUser(string appId)
        {
            try
            {
                _Service.AddUser(appId);
            }
            catch (Exception)
            {
            }
        }
        // deletes specified user
        [HttpDelete]
        [Route("DeleteUser")]
        public void DeleteUser(int userId)
        {
            try
            {
                var result = _Service.GetUser(userId);
                if (result != null)
                {
                    _Service.DeleteUser(userId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        // updates user
        [HttpPost]
        [Route("UpdateUser")]
        public void UpdateUser(User user)
        {
            try
            {
                _Service.UpdateUser(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
