﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Core;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Repositories
{
    public interface ITourPackageRepository : IRepositoryBase<TourPackage, Guid>
    {
        Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId);
    }
}
