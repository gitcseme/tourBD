using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Forum.Contexts;
using tourBD.Forum.Repositories;

namespace tourBD.Forum.UnitOfWorks
{
    public class LikeUnitOfWork : UnitOfWorkBase<ForumContext>, ILikeUnitOfWork
    {
        public LikeUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            Likes = new LikeRepository(_dbContext);
        }
        
        public ILikeRepository  Likes { get; set; }
    }
}
