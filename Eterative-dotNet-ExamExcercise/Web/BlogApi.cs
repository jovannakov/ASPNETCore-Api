using Eterative_dotNet_ExamExcercise.Database;
using Eterative_dotNet_ExamExcercise.Entities;
using Eterative_dotNet_ExamExcercise.Entities.DTO;
using Eterative_dotNet_ExamExcercise.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Web
{
    [Route("blog")]
    [ApiController]
    public class BlogApi : ControllerBase
    {
        private BlogDbContext _context;
        private ILogger _logger;
        private IBlogService _service;

        public BlogApi(BlogDbContext context, ILogger<BlogApi> logger, IBlogService service)
        {
            this._context = context;
            this._logger = logger;
            this._service = service;
        }

        [HttpGet]
        public ActionResult<List<Blog>> GetAllBlogPosts()
        {
            try
            {
                var allBlogs = this._service.GetAllPosts();

                return allBlogs;
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("get")]
        public ActionResult<Blog> GetPostById([FromQuery] int Id)
        {
            try
            {
                return this._service.GetPostById(Id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<Blog> CreateBlogPost([FromBody] BlogModel blog)
        {
            try
            {
                return this._service.CreatePost(blog);
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<Blog> UpdateBlogPost([FromBody] BlogModel blog, [FromQuery]int Id)
        {
            try
            {
                return this._service.UpdatePost(blog, Id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPatch]
        public ActionResult<Blog> UpdateBlogPostTitleOrText([FromQuery] int Id, [FromQuery] string Title = "", [FromQuery] string Text = "")
        {
            try
            {
                return this._service.UpdatePostTextOrTitle(Id, Title, Text);
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public ActionResult<Blog> DeleteBlog([FromQuery] int Id)
        {
            try
            {
                return this._service.DeletePost(Id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
