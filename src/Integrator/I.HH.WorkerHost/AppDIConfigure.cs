using System;
using CG4.DataAccess;
using CG4.DataAccess.Poco;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco.ExprOptions;
using CG4.Impl.Dapper.PostgreSql;
using I.HH.Impl;
using I.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace I.HH.WorkerHost;

public class AppDIConfigure
{
    public static IServiceCollection Configure(IServiceCollection services)
    {
        services.TryAddTransient<ICrudService, AppCrudService>();
        services.TryAddSingleton<IConnectionFactory, ConnectionFactoryPostgreSQL>();
        services.TryAddSingleton<IConnectionSettings, AppSettings>();

        services.TryAddSingleton<ISqlBuilder, ExprSqlBuilder>();
        services.TryAddSingleton<ISqlSettings, PostreSqlOptions>();

        services.AddAutoMapper(typeof(AppAutoMapper));

        services.AddTransient<IHhHelperService,HhHelperService>();
        services.AddHttpClient<IHhHttpClient,HhHttpClient>();

        return services;
    }
}
