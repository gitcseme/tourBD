using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Repositories
{
    public class CommentRepository : RepositoryBase<Comment, Guid, ForumContext>, ICommentRepository
    {
        public CommentRepository(ForumContext context) : base(context)
        {
        }

        public async Task<Comment> GetCommentIncludePropertiesAsync(Guid id)
        {
            return await _DbSet
                .Include(c => c.Replays)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
