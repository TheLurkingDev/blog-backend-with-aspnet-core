using Contracts;
using Entities.Extensions;
using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebsiteServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private ILoggerManager _loggerManager;
        private IRepositoryWrapper _repositoryWrapper;

        public UserController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper)
        {
            _loggerManager = loggerManager;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        #region Auth

        [Route("login")]
        public IActionResult Login([FromBody] Entities.ClientDTOs.Login loginDto)
        {
            var userFromDb = _repositoryWrapper.UserRepository.GetUserByUserName(loginDto.UserName);
            var isAuthenticated = loginDto.VerifyPasswordHash(userFromDb.Salt, userFromDb.HashedPassword);

            return Ok(isAuthenticated);
        }

        #endregion

        #region UserCRUD

        [HttpGet("{id}"), ActionName("GetUserById")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var siteUser = _repositoryWrapper.UserRepository.FindByCondition(user => user.Id == id);
                _loggerManager.LogInfo("Successfully fetched BlogPost from DB");

                return Ok(siteUser);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch User :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers([FromBody]string websiteIdString)
        {
            try
            {
                var websiteId = Guid.Parse(websiteIdString);
                var users = _repositoryWrapper.UserRepository
                    .FindByCondition(user => user.WebsiteID == websiteId)
                    .OrderBy(user => user.DateCreated);

                _loggerManager.LogInfo("Successfully fetched User(s) from DB");

                return Ok(users);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch Users :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]        
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _loggerManager.LogError("User data from client is null");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid User object from client");
                    return BadRequest("User object is invalid");
                }

                var existingWebsite = _repositoryWrapper.WebsiteRepository.FindByCondition(website => website.Id == user.WebsiteID).Any();
                if (!existingWebsite)
                {
                    _loggerManager.LogError("No associated Website found for new User");
                    return BadRequest($"No Website exists with id: {user.WebsiteID.ToString()}");
                }

                _repositoryWrapper.UserRepository.CreateUser(user);

                return CreatedAtAction("GetUserById", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to create a new User :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
    }
}
