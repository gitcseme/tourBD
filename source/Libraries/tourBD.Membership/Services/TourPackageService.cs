using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tourBD.Membership.Entities;
using tourBD.Membership.UnitOfWorks;

namespace tourBD.Membership.Services
{
    public class TourPackageService : ITourPackageService
    {
        private ITourPackageUnitOfWork _tourPackageUnitOfWork { get; }

        public TourPackageService(ITourPackageUnitOfWork tourPackageUnitOfWork)
        {
            _tourPackageUnitOfWork = tourPackageUnitOfWork;
        }

        public async Task CreateAsync(TourPackage entity)
        {
            await _tourPackageUnitOfWork.TourPackageRepository.AddAsync(entity);
            await _tourPackageUnitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(TourPackage entity)
        {
            await _tourPackageUnitOfWork.TourPackageRepository.RemoveAsync(entity);
            await _tourPackageUnitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _tourPackageUnitOfWork.Dispose();
        }

        public async Task EditAsync(TourPackage entity)
        {
            await _tourPackageUnitOfWork.TourPackageRepository.EditAsync(entity);
            await _tourPackageUnitOfWork.SaveAsync();
        }

        public TourPackage Get(Guid Id)
        {
            return _tourPackageUnitOfWork.TourPackageRepository.Get(Id);
        }
        
        public async Task<TourPackage> GetAsync(Guid Id)
        {
            return await _tourPackageUnitOfWork.TourPackageRepository.GetAsync(Id);
        }

        public async Task AddSpot(Spot spot)
        {
            await _tourPackageUnitOfWork.SpotRepository.AddAsync(spot);
            await _tourPackageUnitOfWork.SaveAsync();
        }

        public async Task<TourPackage> GetPackageWithRelatedSpotsAsync(Guid packageId)
        {
            return await _tourPackageUnitOfWork.TourPackageRepository.GetPackageWithRelatedSpotsAsync(packageId);
        }

        public async Task DeleteSpotAsync(Spot spot)
        {
            await _tourPackageUnitOfWork.SpotRepository.RemoveAsync(spot);
            await _tourPackageUnitOfWork.SaveAsync();
        }

        public async Task AddLoveAsync(Love love)
        {
            await _tourPackageUnitOfWork.LoveRepository.AddAsync(love);
            await _tourPackageUnitOfWork.SaveAsync();
        }
    }
}
