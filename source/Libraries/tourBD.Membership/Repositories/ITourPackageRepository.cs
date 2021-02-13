using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;

namespace tourBD.Membership.Repositories
{
    public interface ITourPackageRepository : IRepositoryBase<TourPackage, Guid>
    {
        Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId);
        Task<List<TourPackage>> GetPackagesPaginatedAsync(int pageIndex, int pageSize, BangladeshDivisions selectedDivision);
    }
}
