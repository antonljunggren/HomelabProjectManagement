using Core.Common.Commands;
using Core.Common.Query;
using Core.ServerAggregate;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure
{
    public static class InfrastructureCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            var sqlLiteConnection = new SqliteConnection("DataSource=:memory:");
            sqlLiteConnection.Open();

            services.AddDbContext<WriteDbContext>(opt =>
            {
                opt.UseSqlite(sqlLiteConnection);
                opt.EnableSensitiveDataLogging();
            }, ServiceLifetime.Singleton);

            services.AddDbContext<ReadDbContext>(opt =>
            {
                opt.UseSqlite(sqlLiteConnection);
                opt.EnableSensitiveDataLogging();
            }, ServiceLifetime.Singleton);

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServerRepository, ServerRepository>();

            return services;
        }

        public static IServiceCollection AddCommandQueryServices(this IServiceCollection services)
        {
            services.TryAddScoped<IQueryDispatcher, QueryDispatcher>();
            services.TryAddScoped<ICommandDispatcher, CommandDispatcher>();

            services.Scan(selector =>
            {
                selector.FromCallingAssembly()
                    .AddClasses(filter =>
                    {
                        filter.AssignableTo(typeof(IQueryHandler<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.Scan(selector =>
            {
                selector.FromAssemblyOf<ICommand>()
                    .AddClasses(filter =>
                    {
                        filter.AssignableTo(typeof(ICommandHandler<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.TryDecorate(typeof(ICommandHandler<,>), typeof(LoggingCommandDecorator<,>));

            return services;
        }
    }
}
