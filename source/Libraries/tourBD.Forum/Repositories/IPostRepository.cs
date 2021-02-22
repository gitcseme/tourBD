using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Repositories
{
    public interface IPostRepository : IRepositoryBase<Post, Guid>
    {
        Task<IEnumerable<Post>> GetAllPostsPaginatedAsync(int pageIndex, int pageSize, bool isTrackingOff);

        Task<Post> GetPostIncludePropertiesAsync(Guid id);

        Task<List<Post>> GetRecentPostsAsync(int numberOfPosts = 5);
    }
}
