using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebsiteServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        private ILoggerManager _loggerManager;
        private IRepositoryWrapper _repositoryWrapper;

        public BlogController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper)
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

        [HttpGet("{siteId}")]
        public IActionResult GetAllBlogCategories(Guid siteId)
        {
            try
            {
                var blogCategories = _repositoryWrapper.BlogCategoryRepository
                .FindByCondition(category => category.WebsiteID == siteId)
                .OrderBy(category => category.Name);

                _loggerManager.LogInfo("Successfully fetched BlogCategories from DB");

                return Ok(blogCategories);
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch BlogCategories :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }            
        }

        [HttpGet("{id}"), ActionName("GetCategoryById")]
        [Route("category")]
        public IActionResult GetBlogCategoryById(Guid id)
        {
            try
            {
                var blogCategory = _repositoryWrapper.BlogCategoryRepository.FindByCondition(category => category.Id == id);
                _loggerManager.LogInfo("Successfully fetched BlogCategory from DB");

                return Ok(blogCategory);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch a blog category :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("category")]
        public IActionResult CreateBlogCategory([FromBody] BlogCategory blogCategory)
        {
            try
            {
                if (blogCategory == null)
                {
                    _loggerManager.LogError("BlogCategory data from client is null");
                    return BadRequest("BlogCategory object is null");
                }

                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid BlogCategory object from client");
                    return BadRequest("BlogCategory object is invalid");
                }

                var existingWebsite = _repositoryWrapper.WebsiteRepository.FindByCondition(website => website.Id == blogCategory.WebsiteID).Any();
                if(!existingWebsite)
                {
                    _loggerManager.LogError("No associated website found for new BlogCategory");
                    return BadRequest($"No website exists with id: {blogCategory.WebsiteID.ToString()}");
                }

                _repositoryWrapper.BlogCategoryRepository.CreateBlogCategory(blogCategory);

                return CreatedAtAction("GetCategoryById", new { id = blogCategory.Id }, blogCategory);                
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to create a new blog category :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
