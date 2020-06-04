using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace tourBD.Core
{
    public abstract class UnitOfWorkBase<TContext> : IUnitOfWorkBase
        where TContext : DbContext
    {
        protected readonly TContext _dbContext;

        protected UnitOfWorkBase(string connectionString, string migrationAssemblyName)
        {
            _dbContext = (TContext) Activator.CreateInstance(typeof(TContext), connectionString, migrationAssemblyName);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
