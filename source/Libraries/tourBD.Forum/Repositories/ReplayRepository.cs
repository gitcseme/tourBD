using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Repositories
{
    public class ReplayRepository : RepositoryBase<Replay, Guid, ForumContext>, IReplayRepository
    {
        public ReplayRepository(ForumContext context) : base(context)
        {
        }
    }
}
