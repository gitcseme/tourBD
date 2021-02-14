using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Services
{
    public interface ICompanyService : IService<Company>
    {
        Task<IEnumerable<Company>> GetUserCompaniesAsync(Guid userId);
        Task CreateTourPackage(TourPackage tourPackage);
        Task<Company> GetCompanyWithAllIncludePropertiesAsync(Guid companyId);
        Task<List<Company>> GetAllIncludePropertiesAsync();
    }
}
