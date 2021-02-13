using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;
using tourBD.Membership.Enums;

namespace tourBD.Membership.Services
{
    public interface ITourPackageService : IService<TourPackage>
    {
        Task AddSpot(Spot spot);
        Task DeleteSpotAsync(Spot spot);
        Task AddLoveAsync(Love love);
        Task<int> GetCountAsync();
        Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId);
        Task<List<TourPackage>> GetPackagesPaginatedAsync(int pageIndex, int pageSize, BangladeshDivisions selectedDivision);
    }
}
