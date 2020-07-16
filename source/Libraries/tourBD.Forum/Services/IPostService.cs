﻿using System;
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

        void AddLike(string Id);

        Task<IEnumerable<Post>> GetAllIncludePropertiesAsync(Expression<Func<Post, bool>> filter = null, string orderingColumn = "", string orderDirection = "", string includeProperties = "", bool isTrackingOff = false);
        (IEnumerable<Post>, int, int) GetPosts(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
        Task<(IEnumerable<Post>, int, int)> GetPostsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
    }
}