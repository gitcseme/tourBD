using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
