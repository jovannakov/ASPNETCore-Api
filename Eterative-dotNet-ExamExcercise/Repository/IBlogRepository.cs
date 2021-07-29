using Eterative_dotNet_ExamExcercise.Entities;
using Eterative_dotNet_ExamExcercise.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Repository
{
    public interface IBlogRepository
    {
        Blog GetPostById(int Id);
        List<Blog> GetAllPosts();
        Blog CreatePost(BlogModel entity);
        Blog UpdatePost(BlogModel entity, int Id);
        Blog DeletePost(int Id);
        Blog UpdatePostTextOrTitle(int Id, string Title, string Text);
        void UpdatePostFromEntity(int BlogId, int id);
        void RemoveRelatedPost(int id, int blogId);
        List<BlogModel> GetRelatedPosts(int id);
        ActionResult<BlogModel> CreateNewRelatedPost(int id, BlogModel blog);
    }
}
