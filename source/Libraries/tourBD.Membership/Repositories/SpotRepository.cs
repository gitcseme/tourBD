using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    class SpotRepository : RepositoryBase<Spot, Guid, ApplicationDbContext>, ISpotRepository
    {
        public SpotRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
