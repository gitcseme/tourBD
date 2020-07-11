using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public interface IReplayUnitOfWork
    {
        IReplayRepository Replays { get; }
    }
}
