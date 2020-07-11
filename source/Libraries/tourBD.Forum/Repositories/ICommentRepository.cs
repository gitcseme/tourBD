using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Entities;

namespace tourBD.Forum.Repositories
{
    public interface ICommentRepository : IRepositoryBase<Comment, Guid>
    {
    }
}
