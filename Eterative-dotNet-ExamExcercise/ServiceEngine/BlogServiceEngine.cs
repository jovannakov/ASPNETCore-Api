using Eterative_dotNet_ExamExcercise.Entities;
using Eterative_dotNet_ExamExcercise.Entities.DTO;
using Eterative_dotNet_ExamExcercise.Repository;
using Eterative_dotNet_ExamExcercise.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.ServiceEngine
{
    public class BlogServiceEngine : IBlogService
    {
        private IBlogRepository _repository;

        public BlogServiceEngine(IBlogRepository repository)
        {
            this._repository = repository;
        }

        public ActionResult<BlogModel> CreateNewRelatedPost(int id, BlogModel blog)
        {
            return this._repository.CreateNewRelatedPost(id, blog);
        }

        public Blog CreatePost(BlogModel blog)
        {
            return this._repository.CreatePost(blog);
        }

        public Blog DeletePost(int Id)
        {
            return this._repository.DeletePost(Id);
        }

        public List<Blog> GetAllPosts()
        {
            return this._repository.GetAllPosts();
        }

        public Blog GetPostById(int Id)
        {
            return this._repository.GetPostById(Id);
        }

        public List<BlogModel> GetRelatedPosts(int id)
        {
            return this._repository.GetRelatedPosts(id);
        }

        public void RemoveRelatedPost(int id, int blogId)
        {
            this._repository.RemoveRelatedPost(id, blogId);
        }

        public Blog UpdatePost(BlogModel blog, int Id)
        {
            return this._repository.UpdatePost(blog, Id);
        }

        public void UpdatePostFromEntity(int BlogId, int id)
        {
            this._repository.UpdatePostFromEntity(BlogId, id);
        }

        public Blog UpdatePostTextOrTitle(int Id, string Title, string Text)
        {
            return this._repository.UpdatePostTextOrTitle(Id, Title, Text);
        }
    }
}
