using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public class TourPackageUnitOfWork : UnitOfWorkBase<ApplicationDbContext>, ITourPackageUnitOfWork
    {
        public TourPackageUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            TourPackageRepository = new TourPackageRepository(_dbContext);
            SpotRepository = new SpotRepository(_dbContext);
            LoveRepository = new LoveRepository(_dbContext);
        }

        public ITourPackageRepository TourPackageRepository { get; set; }
        public ISpotRepository SpotRepository { get; set; }
        public ILoveRepository LoveRepository { get; set; }
    }
}
