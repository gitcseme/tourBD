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

        Task AddLikeAsync(Like like);
        Task DeleteLikeAsync(Like like);
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);
        Task AddReplayAsync(Replay replay);
        Task DeleteReplayAsync(Guid replayId);
        string GetRelatedPost(string commentId);

        Task<IEnumerable<Post>> GetAllPostsPaginatedAsync(int pageIndex = 1, int pageSize = 10, bool isTrackingOff = true);
        (IEnumerable<Post>, int, int) GetPosts(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
        Task<(IEnumerable<Post>, int, int)> GetPostsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);

        Task<Post> GetPostIncludePropertiesAsync(Guid id);
        Task<int> GetCountAsync();
    }
}
