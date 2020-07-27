using tourBD.Core;
using tourBD.Membership.Contexts;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public class SpotUnitOfWork : UnitOfWorkBase<ApplicationDbContext>, ISpotUnitOfWork
    {
        public SpotUnitOfWork(string connectionString, string migrationAssemblyName)
            : base(connectionString, migrationAssemblyName)
        {
            SpotRepository = new SpotRepository(_dbContext);
        }

        public ISpotRepository SpotRepository { get; set; }
    }
}
