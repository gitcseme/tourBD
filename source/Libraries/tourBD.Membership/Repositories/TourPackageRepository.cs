using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    public class TourPackageRepository : RepositoryBase<TourPackage, Guid, ApplicationDbContext>, ITourPackageRepository
    {
        public TourPackageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
