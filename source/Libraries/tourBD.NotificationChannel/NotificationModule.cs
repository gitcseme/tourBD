using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using tourBD.NotificationChannel.Contexts;
using tourBD.NotificationChannel.Repositories;
using tourBD.NotificationChannel.Seeds;
using tourBD.NotificationChannel.Services;
using tourBD.NotificationChannel.UnitOfWorks;

namespace tourBD.NotificationChannel
{
    public class NotificationModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public NotificationModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<NotificationRepository>()
                .As<INotificationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NotificationUnitOfWork>()
                .As<INotificationUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<NotificationService>()
                .As<INotificationService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NotificationSeed>().SingleInstance();

            base.Load(builder);
        }
    }
}
