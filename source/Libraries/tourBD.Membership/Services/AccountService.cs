using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public (IEnumerable<ApplicationUser>, int, int) GetUsers(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            var query = _userManager.Users;
            int total = query.Count();
            int totalDisplay = total;

            if (searchText != "")
            {
                query = query.Where(apu => apu.FullName.Contains(searchText));
                totalDisplay = query.Count();
            }

            IEnumerable<ApplicationUser> data;
            IQueryable<ApplicationUser> result;

            if (orderingColumn != "")
            {
                string orderByColDir = (orderingColumn + (orderDirection != "" ? $" {orderDirection}" : ""));
                result = query.OrderBy(orderByColDir).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
                result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            if (isTrackingOff)
                data = result.AsNoTracking().ToList();
            else
                data = result.ToList();

            return (data, total, totalDisplay);
        }

        public async Task<(IEnumerable<ApplicationUser>, int, int)> GetUsersAsync(int pageIndex, int pageSize, bool isTrackingOff, string searchText, string orderingColumn, string orderDirection)
        {
            var query = _userManager.Users;
            int total = query.Count();
            int totalDisplay = total;

            if (searchText != "")
            {
                query = query.Where(apu => apu.FullName.Contains(searchText));
                totalDisplay = query.Count();
            }

            IEnumerable<ApplicationUser> data;
            IQueryable<ApplicationUser> result;

            if (orderingColumn != "")
            {
                string orderByColDir = (orderingColumn + (orderDirection != "" ? $" {orderDirection}" : ""));
                result = query.OrderBy(orderByColDir).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
                result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            if (isTrackingOff)
                data = await result.AsNoTracking().ToListAsync();
            else
                data = await result.ToListAsync();

            return (data, total, totalDisplay);
        }
    }
}
