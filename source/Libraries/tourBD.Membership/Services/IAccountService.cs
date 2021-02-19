using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Services
{
    public interface IAccountService
    {
        (IEnumerable<ApplicationUser>, int, int) GetUsers(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
        Task<(IEnumerable<ApplicationUser>, int, int)> GetUsersAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection);
    }
}
