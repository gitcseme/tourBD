using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace tourBD.Core.Seeds
{
    public abstract class DataSeed : Iseed
    {
        protected readonly DbContext dbContext;
        public DataSeed(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await dbContext.Database.MigrateAsync();
        }

        public abstract Task SeedAsync();
    }
}
