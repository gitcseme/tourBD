using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
