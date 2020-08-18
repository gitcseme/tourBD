using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tourBD.Core.Seeds
{
    public interface Iseed
    {
        Task MigrateAsync();
        Task SeedAsync();
    }
}
