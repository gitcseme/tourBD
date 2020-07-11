using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public class ReplayUnitOfWork : UnitOfWorkBase<ForumContext>, IReplayUnitOfWork
    {
        public ReplayUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            Replays = new ReplayRepository(_dbContext);
        }

        public IReplayRepository Replays { get; protected set; }
    }
}
