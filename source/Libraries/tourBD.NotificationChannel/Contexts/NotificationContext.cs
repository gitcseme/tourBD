using Microsoft.EntityFrameworkCore;
using tourBD.NotificationChannel.Entities;

namespace tourBD.NotificationChannel.Contexts
{
    public class NotificationContext : DbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyname;

        public NotificationContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyname = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    _connectionString,
                    b => b.MigrationsAssembly(_migrationAssemblyname)
                );
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Notification> Notifications { get; set; }
    }
}
