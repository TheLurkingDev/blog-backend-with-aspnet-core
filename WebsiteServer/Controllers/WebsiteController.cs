using Contracts;
using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebsiteServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : Controller
    {
        private ILoggerManager _loggerManager;
        private IRepositoryWrapper _repositoryWrapper;

        public WebsiteController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper)
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

        [HttpGet]
        public IActionResult GetAllWebsites()
        {
            try
            {
                var websites = _repositoryWrapper.WebsiteRepository.GetAllWebsites();
                _loggerManager.LogInfo("Successfully fetched all Websites from DB");

                return Ok(websites);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch all Websites :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}"), ActionName("GetWebsiteById")]
        public IActionResult GetWebsiteById(Guid id)
        {
            try
            {
                var website = _repositoryWrapper.WebsiteRepository.GetWebsiteById(id);
                _loggerManager.LogInfo("Successfully fetched Website from DB");

                return Ok(website);
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch a website :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWebsite(Guid id, [FromBody] Website website)
        {
            try
            {
                if (website == null)
                {
                    _loggerManager.LogError("Website data from client is null");
                    return BadRequest("Website object is null");
                }

                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid Website object from client");
                    return BadRequest("Website object is invalid");
                }

                var dbWebsite = _repositoryWrapper.WebsiteRepository.GetWebsiteById(id);
                if(dbWebsite == null)
                {
                    _loggerManager.LogError($"Error updating Website. No Website exists with id: {id}");
                    return BadRequest($"No Website Exists with id: {id}");
                }

                _repositoryWrapper.WebsiteRepository.UpdateWebsite(dbWebsite, website);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to update a website :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult CreateWebsite([FromBody] Website website)
        {
            try
            {
                if (website == null)
                {
                    _loggerManager.LogError("Website data from client is null");
                    return BadRequest("Website object is null");
                }

                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid Website object from client");
                    return BadRequest("Website object is invalid");
                }

                _repositoryWrapper.WebsiteRepository.CreateWebsite(website);

                return CreatedAtAction("GetWebsiteById", new { id = website.Id }, website);
                
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to create a new website :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWebsite(Guid id)
        {
            try
            {
                var website = _repositoryWrapper.WebsiteRepository.GetWebsiteById(id);

                if (website == null)
                {
                    _loggerManager.LogError($"Website with id {id} does not exist");
                    return NotFound();
                }

                _repositoryWrapper.WebsiteRepository.DeleteWebsite(website);
                return NoContent();
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to delete Website :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
