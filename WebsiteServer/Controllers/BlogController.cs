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

        #region BlogCategory

        
        [HttpGet]
        [Route("categories")]
        public IActionResult GetAllBlogCategories([FromBody]string websiteIdString)
        {
            try
            {
                var websiteId = Guid.Parse(websiteIdString);
                var blogCategories = _repositoryWrapper.BlogCategoryRepository
                    .FindByCondition(category => category.WebsiteID == websiteId)
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

        [HttpGet("category/{id}"), ActionName("GetCategoryById")]        
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

        [HttpPut("category/{id}")]
        public IActionResult UpdateBlogCategory(Guid id, [FromBody] BlogCategory blogCategory)
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

                var dbBlogCategory = _repositoryWrapper.BlogCategoryRepository.GetBlogCategoryById(id);
                if(dbBlogCategory == null)
                {
                    _loggerManager.LogError($"Unable to update BlogCategory. BlogCategory with id {id} does not exist.");
                    return NotFound();
                }

                _repositoryWrapper.BlogCategoryRepository.UpdateBlogCategory(dbBlogCategory, blogCategory);
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Unable to update BlogCategory :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("category/{id}")]
        public IActionResult DeleteBlogCategory(Guid id)
        {
            try
            {
                var blogCategory = _repositoryWrapper.BlogCategoryRepository.GetBlogCategoryById(id);

                if (blogCategory == null)
                {
                    _loggerManager.LogError($"BlogCategory with id {id} does not exist.");
                    return NotFound();
                }

                _repositoryWrapper.BlogCategoryRepository.DeleteBlogCategory(blogCategory);
                return NoContent();
            }
            catch(Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to delete BlogCategory :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion

        #region BlogPost

        [HttpGet("post/{id}"), ActionName("GetBlogPostById")]        
        public IActionResult GetBlogPostById(Guid id)
        {
            try
            {
                var blogPost = _repositoryWrapper.BlogPostRepository.FindByCondition(post => post.Id == id);
                _loggerManager.LogInfo("Successfully fetched BlogPost from DB");

                return Ok(blogPost);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch a blog post :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("posts")]
        public IActionResult GetAllBlogPosts([FromBody]string categoryIdString)
        {
            try
            {
                var categoryId = Guid.Parse(categoryIdString);
                var blogPosts = _repositoryWrapper.BlogPostRepository
                    .FindByCondition(post => post.BlogCategoryID == categoryId)
                    .OrderBy(post => post.DateCreated);

                _loggerManager.LogInfo("Successfully fetched BlogPost(s) from DB");

                return Ok(blogPosts);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"An error occurred while attempting to fetch BlogPosts :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("post")]
        public IActionResult CreateBlogPost([FromBody] BlogPost blogPost)
        {
            try
            {
                if (blogPost == null)
                {
                    _loggerManager.LogError("BlogPost data from client is null");
                    return BadRequest("BlogPost object is null");
                }

                if (!ModelState.IsValid)
                {
                    _loggerManager.LogError("Invalid BlogPost object from client");
                    return BadRequest("BlogPost object is invalid");
                }

                var existingBlogCategory = _repositoryWrapper.BlogCategoryRepository.FindByCondition(category => category.Id == blogPost.BlogCategoryID).Any();
                if (!existingBlogCategory)
                {
                    _loggerManager.LogError("No associated BlogCategory found for new BlogPost");
                    return BadRequest($"No BlogCategory exists with id: {blogPost.BlogCategoryID.ToString()}");
                }

                _repositoryWrapper.BlogPostRepository.CreateBlogPost(blogPost);

                return CreatedAtAction("GetBlogPostById", new { id = blogPost.Id }, blogPost);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Error occurred while attempting to create a new BlogPost :: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
    }
}
