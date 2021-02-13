using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;

namespace tourBD.Membership.Repositories
{
    public class TourPackageRepository : RepositoryBase<TourPackage, Guid, ApplicationDbContext>, ITourPackageRepository
    {
        public TourPackageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<TourPackage>> GetPackagesPaginatedAsync(int pageIndex, int pageSize, BangladeshDivisions selectedDivision)
        {
            var query = _DbSet
                .Include(tp => tp.Spots)
                .Include(tp => tp.Loves)
                .Where(tp => tp.Division == selectedDivision.ToString()).AsNoTracking();

            var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return await result.ToListAsync();
        }

        public async Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId)
        {
            return 
                await _DbSet
                .Where(p => p.Id == packageId)
                .Include(p => p.Spots)
                .Include(p => p.Loves)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
