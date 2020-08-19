using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public interface IPostUnitOfWork : IUnitOfWorkBase
    {
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ILikeRepository LikeRepository { get; }
        IReplayRepository ReplayRepository { get; }
    }
}
