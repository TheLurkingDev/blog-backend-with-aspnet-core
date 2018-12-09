using Contracts;
using Entities.Extensions;
using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using SecurityService;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ILoggerManager _loggerManager;
        private IConfiguration _configuration;
        private IRepositoryWrapper _repositoryWrapper;

        public UserController(ILoggerManager loggerManager, IConfiguration configuration, IRepositoryWrapper repositoryWrapper)
        {
            _loggerManager = loggerManager;
            _configuration = configuration;
            _repositoryWrapper = repositoryWrapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        #region Auth

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Entities.ClientDTOs.Login loginDto)
        {
            IActionResult response = Unauthorized();

            var userFromDb = _repositoryWrapper.UserRepository.GetUserByUserName(loginDto.UserName);
            var isAuthenticated = loginDto.VerifyPasswordHash(userFromDb.Salt, userFromDb.HashedPassword);

            if(isAuthenticated)
            {
                var tokenString = Jwt.BuildToken(_configuration, userFromDb.Role);
                response = Ok(new { token = tokenString });
            }

            return response;
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

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _loggerManager.LogError("User data from client is null");
                    return BadRequest("Website object is null");
                }

                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid User object from client");
                    return BadRequest("Website object is invalid");
                }

                var dbUser = _repositoryWrapper.UserRepository.GetUserById(id);
                if (dbUser == null)
                {
                    _loggerManager.LogError($"Error updating User. No User exists with id: {id}");
                    return BadRequest($"No User Exists with id: {id}");
                }

                _repositoryWrapper.UserRepository.UpdateUser(dbUser, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to update a User :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("user/{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _repositoryWrapper.UserRepository.GetUserById(id);

                if (user == null)
                {
                    _loggerManager.LogError($"User with id {id} does not exist.");
                    return NotFound();
                }

                _repositoryWrapper.UserRepository.DeleteUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to delete User :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
    }
}
