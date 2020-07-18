using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Services
{
    public interface IPostService : IService<Post>
    {
        Task<IEnumerable<Post>> GetAllAsync();
        IEnumerable<Post> GetAll();

        Task AddLikeAsync(Post post, Like like);

        Task<IEnumerable<Post>> GetAllIncludePropertiesAsync();
        (IEnumerable<Post>, int, int) GetPosts(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
        Task<(IEnumerable<Post>, int, int)> GetPostsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
    }
}
