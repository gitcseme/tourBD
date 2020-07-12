using Autofac;
using tourBD.Forum.Contexts;

namespace tourBD.Forum
{
    public class ForumModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ForumModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForumContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
