using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    public class CompanyRepository : RepositoryBase<Company, Guid, ApplicationDbContext>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Company> GetWithAllIncludePropertiesAsync(Guid companyId)
        {
            return await Task.Run(() =>
            {
                return _DbSet
                        .Where(c => c.Id == companyId)
                        .Include(c => c.TourPackages).ThenInclude(tp => tp.Spots)
                        .Include(c => c.TourPackages).ThenInclude(tp => tp.Loves)
                        .AsNoTracking()
                        .FirstOrDefault();
            });
        }
    }
}
