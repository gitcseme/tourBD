using tourBD.Core;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public interface ICompanyUnitOfWork : IUnitOfWorkBase
    {
        ICompanyRepository CompanyRepository { get; }
        ITourPackageRepository TourPackageRepository { get; }
    }
}
