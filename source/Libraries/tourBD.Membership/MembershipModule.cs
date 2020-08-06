using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Membership.Contexts;
using tourBD.Membership.Repositories;
using tourBD.Membership.Seeds;
using tourBD.Membership.Services;
using tourBD.Membership.UnitOfWorks;

namespace tourBD.Membership
{
    public class MembershipModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public MembershipModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>()
                .As<ICompanyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TourPackageRepository>()
                .As<ITourPackageRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SpotRepository>()
                .As<ISpotRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRequestRepository>()
                .As<ICompanyRequestRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyUnitOfWork>()
                .As<ICompanyUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<TourPackageUnitOfWork>()
                .As<ITourPackageUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<SpotUnitOfWork>()
                .As<ISpotUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRequestUnitOfWork>()
                .As<ICompanyRequestUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRequestService>()
                .As<ICompanyRequestService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>()
                .As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthoritySeed>().SingleInstance();
            base.Load(builder);
        }
    }
}
