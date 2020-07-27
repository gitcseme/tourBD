using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    public class CompanyRequestRepository : RepositoryBase<CompanyRequest, Guid, ApplicationDbContext>, ICompanyRequestRepository
    {
        public CompanyRequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
