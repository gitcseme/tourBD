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

        public async Task<IEnumerable<Post>> GetAllIncludePropertiesAsync()
        {
            return await Task.Run(() => {
                return _DbSet
                    .Include(post => post.Comments)
                        .ThenInclude(cmt => cmt.Replays)
                    .Include(post => post.Likes)
                    .AsNoTracking()
                    .ToList();
            }); 
        }
    }
}
