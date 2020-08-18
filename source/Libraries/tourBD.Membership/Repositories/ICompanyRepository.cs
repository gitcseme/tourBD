using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company, Guid>
    {
        Task<Company> GetWithAllIncludePropertiesAsync(Guid companyId);
    }
}
