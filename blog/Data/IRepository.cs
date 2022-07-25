using blog.Models;
using blog.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blog.Data.Repository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllPosts(string Category);
        void AddPost(Post post);
        void RemovePost(int id);

        void UpdatePost(Post post);

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();
    }
}
