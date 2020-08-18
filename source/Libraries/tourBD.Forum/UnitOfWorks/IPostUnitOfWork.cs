using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public interface IPostUnitOfWork : IUnitOfWorkBase
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        ILikeRepository Likes { get; }
    }
}
