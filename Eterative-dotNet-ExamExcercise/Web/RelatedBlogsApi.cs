using Eterative_dotNet_ExamExcercise.Entities.DTO;
using Eterative_dotNet_ExamExcercise.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eterative_dotNet_ExamExcercise.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatedBlogsApi : ControllerBase
    {
        IBlogService _service;

        public RelatedBlogsApi(IBlogService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Gets all related posts.
        /// </summary>
        /// <param name="id">The id of the post that is relating.</param>
        /// <returns></returns>
        // GET api/<RelatedBlogsApi>/5
        [HttpGet("{id}")]
        public ActionResult<List<BlogModel>> Get(int id)
        {
            try
            {
                return this._service.GetRelatedPosts(id);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Create new blog that is related to the blog with Id that is
        /// same with the one passed from the Uri.
        /// </summary>
        /// <param name="id">The id of the post that is relating.</param>
        /// <param name="BlogId">The id of the post that is related to.</param>
        /// <returns></returns>
        // POST api/<RelatedBlogsApi>
        [HttpPost("{id}")]
        public ActionResult<BlogModel> Post(int id, [FromBody] BlogModel blog)
        {
            try
            {
                return this._service.CreateNewRelatedPost(id, blog);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Given that the Blog that has the same id as BlogId
        /// exists in the database, with this PUT call we are 
        /// adding a related post to the blog that has the same id as id.
        /// </summary>
        /// <param name="id">The id of the post that is relating.</param>
        /// <param name="BlogId">The id of the post that is related to.</param>
        /// <returns></returns>
        // PUT api/<RelatedBlogsApi>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] int BlogId)
        {
            try
            {
                this._service.UpdatePostFromEntity(BlogId, id);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return this.StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Given that both posts exist in the db, with the id's given as parameters,
        /// we will remove the relation between these two blog posts.
        /// </summary>
        /// <param name="id">The id of the post that is relating.</param>
        /// <param name="BlogId">The id of the post that is related to.</param>
        /// <returns></returns>
        // DELETE api/<RelatedBlogsApi>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromBody] int BlogId)
        {
            try
            {
                this._service.RemoveRelatedPost(id, BlogId);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return this.StatusCode(StatusCodes.Status200OK);
        }
    }
}
