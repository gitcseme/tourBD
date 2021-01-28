using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Services
{
    public interface ITourPackageService : IService<TourPackage>
    {
        Task AddSpot(Spot spot);
        Task DeleteSpotAsync(Spot spot);
        Task AddLoveAsync(Love love);
        Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId);
    }
}
