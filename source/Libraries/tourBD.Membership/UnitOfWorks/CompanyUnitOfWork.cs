using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public class CompanyUnitOfWork : UnitOfWorkBase<ApplicationDbContext>, ICompanyUnitOfWork
    {
        public CompanyUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            CompanyRepository = new CompanyRepository(_dbContext);
            TourPackageRepository = new TourPackageRepository(_dbContext);
        }

        public ICompanyRepository CompanyRepository { get; set; }
        public ITourPackageRepository TourPackageRepository { get; set; }
    }
}
