using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public class CompanyRequestUnitOfWork : UnitOfWorkBase<ApplicationDbContext>, ICompanyRequestUnitOfWork
    {
        public CompanyRequestUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            CompanyRequestRepository = new CompanyRequestRepository(_dbContext);
        }

        public ICompanyRequestRepository CompanyRequestRepository { get; set; }
    }
}
