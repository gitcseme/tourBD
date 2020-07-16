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
            await _postUnitOfWork.Posts.AddAsync(entity);
            await _postUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Post entity)
        {
            await _postUnitOfWork.Posts.RemoveAsync(entity);
            await _postUnitOfWork.SaveAsync();
        }

        public async Task EditAsync(Post entity)
        {
            await _postUnitOfWork.Posts.EditAsync(entity);
            await _postUnitOfWork.SaveAsync();
        }

        public Post Get(Guid Id)
        {
            return _postUnitOfWork.Posts.Get(Id);
        }

        public void Dispose()
        {
            _postUnitOfWork.Dispose();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postUnitOfWork.Posts.GetAllAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _postUnitOfWork.Posts.GetAll();
        }

        public (IEnumerable<Post>, int, int) GetPosts(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            return _postUnitOfWork.Posts.Get(null, "", "", "", pageIndex, pageSize, true);
        }

        public async Task<(IEnumerable<Post>, int, int)> GetPostsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            return await _postUnitOfWork.Posts.GetAsync(null, "", "", "", pageIndex, pageSize, true);
        }

        public void AddLike(string Id)
        {
            
        }

        public async Task<IEnumerable<Post>> GetAllIncludePropertiesAsync(Expression<Func<Post, bool>> filter = null, string orderingColumn = "", string orderDirection = "", string includeProperties = "", bool isTrackingOff = false)
        {
            return await _postUnitOfWork.Posts.GetAsync(filter, orderingColumn, orderDirection, includeProperties, isTrackingOff);
        }
    }
}
