using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public class PostUnitOfWork : UnitOfWorkBase<ForumContext>, IPostUnitOfWork
    {
        public PostUnitOfWork(string connectionString, string migrationAssemblyName) 
            : base(connectionString, migrationAssemblyName)
        {
            PostRepository = new PostRepository(_dbContext);
            CommentRepository = new CommentRepository(_dbContext);
            LikeRepository = new LikeRepository(_dbContext);
            ReplayRepository = new ReplayRepository(_dbContext);
        }

        public IPostRepository PostRepository { get; protected set; }
        public ICommentRepository CommentRepository { get; protected set; }
        public ILikeRepository LikeRepository { get; protected set; }
        public IReplayRepository ReplayRepository { get; protected set; }
    }
}
