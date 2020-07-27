using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Services
{
    public interface ICompanyRequestService : IService<CompanyRequest>
    {
        (IEnumerable<CompanyRequest>, int, int) GetRequests(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
        Task<(IEnumerable<CompanyRequest>, int, int)> GetRequestsAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
    }
}
