using tourBD.Core;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public interface ICompanyRequestUnitOfWork : IUnitOfWorkBase
    {
        ICompanyRequestRepository CompanyRequestRepository { get; }
    }
}
