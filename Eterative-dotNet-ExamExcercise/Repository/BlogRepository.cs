using Eterative_dotNet_ExamExcercise.Database;
using Eterative_dotNet_ExamExcercise.Entities;
using Eterative_dotNet_ExamExcercise.Entities.DTO;
using Eterative_dotNet_ExamExcercise.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private BlogDbContext _context;
        private ILogger _logger;

        public BlogRepository(BlogDbContext context, ILogger<BlogRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public ActionResult<BlogModel> CreateNewRelatedPost(int id, BlogModel entity)
        {
            if (string.IsNullOrEmpty(entity.Text) || string.IsNullOrEmpty(entity.Title)) throw new RequiredFieldsException();

            try
            {
                var blog = new Blog()
                {
                    Title = entity.Title,
                    Text = entity.Text,
                    CreatedOn = DateTime.Now
                };

                this._context.Blog.Add(blog);
                this._context.SaveChanges();

                var nextId = this._context.Blog
                    .OrderByDescending(x => x.Id)
                    .First().Id;
                
                this._context.RelatedBlogs.Add(new RelatedBlogs() { BaseBlog = id, RelatedTo = nextId });

                this._context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return new BlogModel();
            }
        }

        public Blog CreatePost(BlogModel entity)
        {
            if (string.IsNullOrEmpty(entity.Text) || string.IsNullOrEmpty(entity.Title)) throw new RequiredFieldsException();

            try
            {
                var blog = new Blog()
                {
                    Title = entity.Title,
                    Text = entity.Text,
                    CreatedOn = DateTime.Now
                };
                this._context.Blog.Add(blog);
                this._context.SaveChanges();
                return blog;
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return new Blog();
            }
        }

        public Blog DeletePost(int Id)
        {
            Blog entity = null;
            try
            {
                entity = this._context.Blog.Find(Id);
                entity.DeletedOn = DateTime.Now;
                this._context.Blog.Update(entity);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
            }

            return entity;
        }

        public List<Blog> GetAllPosts()
        {
            try
            {
                return this._context.Blog.OrderByDescending(x => x.CreatedOn).ToList();
            }
            catch(Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
            }
            return new List<Blog>();
        }

        public Blog GetPostById(int Id)
        {
            try
            {
                return this._context.Blog.Find(Id);
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
            }

            return null;
        }

        public List<BlogModel> GetRelatedPosts(int id)
        {
            List<BlogModel> relatedPosts = new List<BlogModel>();
            try
            {
                var blog = this._context.Blog.Find(id);
                var relatedBlogs = this._context.RelatedBlogs.Where(x => x.BaseBlog == id);
                foreach(var obj in relatedBlogs)
                {
                    obj.RelatedToObj = this._context.Blog.Find(obj.RelatedTo);
                    relatedPosts.Add(new BlogModel() { Title = obj.RelatedToObj.Title, Text = obj.RelatedToObj.Text });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
            }

            return relatedPosts;
        }

        public void RemoveRelatedPost(int id, int blogId)
        {
            try
            {
                this._context.RelatedBlogs.Remove(new RelatedBlogs() { BaseBlog = id, RelatedTo = blogId });
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
            }
        }

        public Blog UpdatePost(BlogModel entity, int Id)
        {
            if (string.IsNullOrEmpty(entity.Text) || string.IsNullOrEmpty(entity.Title)) throw new RequiredFieldsException();

            try
            {
                var blog = this._context.Blog.Find(Id);
                blog.Title = entity.Title != blog.Title ? entity.Title : blog.Title;
                blog.Text = entity.Text != blog.Text ? entity.Text : blog.Text;
                blog.ModifiedOn = DateTime.Now;
                this._context.Blog.Update(blog);
                this._context.SaveChanges();
                return blog;
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return new Blog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BlogId">The id of the Blog we want to relate to.</param>
        /// <param name="id">The id of the Blog we want to add blog that this one is relating to.</param>
        public void UpdatePostFromEntity(int BlogId, int id)
        {
            try
            {
                var obj = this._context.Blog.Find(id);
                obj.RelatedBlogs.Add(new Entities.RelatedBlogs() { BaseBlog = id, RelatedTo = BlogId });
                this._context.Blog.Update(obj);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
            }
        }

        public Blog UpdatePostTextOrTitle(int Id, string Title, string Text)
        {
            try
            {
                var blog = this._context.Blog.Find(Id);
                blog.Title = !string.IsNullOrEmpty(Title) && Title != blog.Title ? Title : blog.Title;
                blog.Text = !string.IsNullOrEmpty(Text) && Text != blog.Text ? Text : blog.Text;
                blog.ModifiedOn = DateTime.Now;
                this._context.Blog.Update(blog);
                this._context.SaveChanges();
                return blog;
            }
            catch (Exception ex)
            {
                this._logger.LogError(String.Format("EXCEPTION: {0}", ex.Message));
                return new Blog();
            }
        }
    }
}
