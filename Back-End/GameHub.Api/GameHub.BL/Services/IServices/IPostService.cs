using GameHub.Common.Entities;

namespace GameHub.BL.Services.IServices
{
    public interface IPostService : IBaseService
    {

        public Post FindPostById(string Id);
        public Post FindPostByTopic(string topic);
        public List<Post> FindPostsByCreator(User creator);
        public Task<Post> AddPostAsync(Post post);
        public Task RemovePostAsync(Post post);
        public Task RemovePostByIdAsync(string id);
        public List<Post> FindAll();
        public int Count(string category = "");
        public List<Post> FindAll(int? index, int pagesize, string category = "");
    }
}
