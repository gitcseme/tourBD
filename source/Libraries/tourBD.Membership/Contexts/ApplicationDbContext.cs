using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tourBD.Membership.Entities;

namespace tourBD.Membership.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyname;

        public ApplicationDbContext(string connectionString, string migrationAssemblyName)
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TourPackage>()
                .HasOne(tp => tp.Company)
                .WithMany(c => c.TourPackages)
                .HasForeignKey(tp => tp.CompanyId);

            builder.Entity<Spot>()
                .HasOne(s => s.TourPackage)
                .WithMany(tp => tp.Spots)
                .HasForeignKey(s => s.TourPackageId);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Spot> Spots { get; set; }
    }
}
