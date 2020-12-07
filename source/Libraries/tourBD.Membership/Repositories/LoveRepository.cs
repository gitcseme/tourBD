using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    public class LoveRepository : RepositoryBase<Love, Guid, ApplicationDbContext>, ILoveRepository
    {
        public LoveRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
