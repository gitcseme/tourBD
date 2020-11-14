using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Forum.Entities;
using tourBD.Forum.UnitOfWorks;
using System.Linq;
using System.Linq.Expressions;

namespace tourBD.Forum.Services
{
    public class PostService : IPostService
    {
        private IPostUnitOfWork _postUnitOfWork { get; }

        public PostService(IPostUnitOfWork postUnitOfWork)
        {
            _postUnitOfWork = postUnitOfWork;
        }

        public async Task CreateAsync(Post entity)
        {
            await _postUnitOfWork.PostRepository.AddAsync(entity);
            await _postUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Post entity)
        {
            await _postUnitOfWork.PostRepository.RemoveAsync(entity);
            await _postUnitOfWork.SaveAsync();
        }

        public async Task EditAsync(Post entity)
        {
            await _postUnitOfWork.PostRepository.EditAsync(entity);
            await _postUnitOfWork.SaveAsync();
        }

        public Post Get(Guid Id)
        {
            return _postUnitOfWork.PostRepository.Get(Id);
        }

        public void Dispose()
        {
            _postUnitOfWork.Dispose();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postUnitOfWork.PostRepository.GetAllAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _postUnitOfWork.PostRepository.GetAll();
        }

        public (IEnumerable<Post>, int, int) GetPosts(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            return _postUnitOfWork.PostRepository.Get(null, "", "", "", pageIndex, pageSize, true);
        }

        public async Task<(IEnumerable<Post>, int, int)> GetPostsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            return await _postUnitOfWork.PostRepository.GetAsync(null, "", "", "", pageIndex, pageSize, true);
        }

        public async Task AddLikeAsync(Like like)
        {
                await _postUnitOfWork.LikeRepository.AddAsync(like);
                await _postUnitOfWork.SaveAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
                await _postUnitOfWork.CommentRepository.AddAsync(comment);
                await _postUnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsPaginatedAsync(int pageIndex = 1, int pageSize = 10, bool isTrackingOff = true)
        {
            return await _postUnitOfWork.PostRepository.GetAllPostsPaginatedAsync(pageIndex, pageSize, isTrackingOff);
        }

        public async Task AddReplayAsync(Replay replay)
        {
            await _postUnitOfWork.ReplayRepository.AddAsync(replay);
            await _postUnitOfWork.SaveAsync();
        }

        public async Task DeleteReplayAsync(Guid replayId)
        {
            await _postUnitOfWork.ReplayRepository.RemoveAsync(replayId);
            await _postUnitOfWork.SaveAsync();
        }
    }
}
