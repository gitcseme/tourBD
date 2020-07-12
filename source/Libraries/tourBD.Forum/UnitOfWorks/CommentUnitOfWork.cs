using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public class CommentUnitOfWork : UnitOfWorkBase<ForumContext>, ICommentUnitOfWork
    {
        public CommentUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            Comments = new CommentRepository(_dbContext);
            Replays = new ReplayRepository(_dbContext);
        }

        public ICommentRepository Comments { get; protected set; }
        public IReplayRepository Replays { get; protected set; }
    }
}
