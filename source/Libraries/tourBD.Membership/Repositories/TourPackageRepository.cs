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
    public class TourPackageRepository : RepositoryBase<TourPackage, Guid, ApplicationDbContext>, ITourPackageRepository
    {
        public TourPackageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId)
        {
            return await Task.Run(() =>
            {
                return _DbSet
                        .Where(p => p.Id == packageId)
                        .Include(p => p.Spots)
                        .Include(p => p.Loves)
                        .AsNoTracking()
                        .FirstOrDefault();
            });
        }
    }
}
