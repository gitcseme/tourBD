using Autofac;
using tourBD.Forum.Contexts;
using tourBD.Forum.Repositories;
using tourBD.Forum.Services;
using tourBD.Forum.UnitOfWorks;

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

            builder.RegisterType<PostRepository>()
                .As<IPostRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CommentRepository>()
                .As<ICommentRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ReplayRepository>()
                .As<IReplayRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LikeRepository>()
                .As<ILikeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PostUnitOfWork>()
                .As<IPostUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentUnitOfWork>()
                .As<ICommentUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ReplayUnitOfWork>()
                .As<IReplayUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<LikeUnitOfWork>()
                .As<ILikeUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<PostService>()
                .As<IPostService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
