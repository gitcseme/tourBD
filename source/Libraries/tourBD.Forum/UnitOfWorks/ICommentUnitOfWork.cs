using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public interface ICommentUnitOfWork : IUnitOfWorkBase
    {
        ICommentRepository Comments { get; }
        IReplayRepository Replays { get; }
    }
}
