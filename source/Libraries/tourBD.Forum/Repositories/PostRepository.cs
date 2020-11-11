using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Repositories
{
    public class PostRepository : RepositoryBase<Post, Guid, ForumContext>, IPostRepository
    {
        public PostRepository(ForumContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetAllPostsPaginatedAsync(int pageIndex = 1, int pageSize = 10, bool isTrackingOff = true)
        {
            IQueryable<Post> query = _DbSet;
            query = query
                    .Include(post => post.Comments)
                        .ThenInclude(cmt => cmt.Replays)
                    .Include(post => post.Likes);

            var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            IEnumerable<Post> data;

            if (isTrackingOff)
                data = await result.AsNoTracking().ToListAsync();
            else
                data = await result.ToListAsync();

            return data;
        }
    }
}
