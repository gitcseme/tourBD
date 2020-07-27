using tourBD.Core;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public interface ISpotUnitOfWork : IUnitOfWorkBase
    {
        ISpotRepository SpotRepository { get; }
    }
}
