using System;
using System.Threading.Tasks;

namespace tourBD.Core
{
    public interface IUnitOfWorkBase : IDisposable
    {
        Task SaveAsync();
        void Save();
    }
}
