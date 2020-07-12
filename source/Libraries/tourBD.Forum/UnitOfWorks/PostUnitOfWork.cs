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
            Posts = new PostRepository(_dbContext);
            Comments = new CommentRepository(_dbContext);
        }

        public IPostRepository Posts { get; protected set; }
        public ICommentRepository Comments { get; protected set; }
    }
}
