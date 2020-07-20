using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Repositories
{
    public class LikeRepository : RepositoryBase<Like, Guid, ForumContext>, ILikeRepository
    {
        public LikeRepository(ForumContext context) : base(context)
        {
        }
    }
}
