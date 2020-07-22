using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using tourBD.Membership.Contexts;
using tourBD.Membership.Seeds;

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

            builder.RegisterType<AuthoritySeed>().SingleInstance();

            base.Load(builder);
        }
    }
}
