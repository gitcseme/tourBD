using tourBD.Core;
using tourBD.Membership.Repositories;

namespace tourBD.Membership.UnitOfWorks
{
    public interface ITourPackageUnitOfWork : IUnitOfWorkBase
    {
        ITourPackageRepository TourPackageRepository { get; }
        ISpotRepository SpotRepository { get; }
        ILoveRepository LoveRepository { get; }
    }
}
