using Asp.Versioning;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NecoTemplate.Application.Abstractions.Caching;
using NecoTemplate.Application.Abstractions.Clock;
using NecoTemplate.Application.Abstractions.Database;
using NecoTemplate.Domain.Models.Examples;
using NecoTemplate.Infrastructure.Clock;
using NecoTemplate.Infrastructure.Database;
using NecoTemplate.Infrastructure.Repositories;
using Npgsql;
using Serilog.Sinks.OpenSearch;
using Serilog;
using MassTransit;
using NecoTemplate.Infrastructure.Caching;
using Serilog.Formatting.Json;
using Microsoft.AspNetCore.Builder;
using NecoTemplate.gRPC;

namespace NecoTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddPersistence(configuration);

        services.AddCaching(configuration);

        services.AddHealthChecks( configuration);
        services.AddgRPC(configuration);
        AddLogStack();

        return services;
    }

    public static void AddgRPC(this WebApplication app)
    {
        // app.MapGrpcService<GreeterService>();
    }
    private static void AddgRPC(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();

        var address = configuration?.GetSection("gRPC").GetSection("nAuth") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddScoped<IAuthService>(provider =>
        {
            return new nAuthClient(address.Value+"");
        });

    }
    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IExampleRepository, ExampleRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        //services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        //SqlMapper.AddTypeHandler(new ());
    }

    private static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.TryAddSingleton<ICacheService, CacheService>();
    }

    private static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!);
    }

    //private static void AddApiVersioning(this IServiceCollection services)
    //{
    //    services
    //        .AddApiVersioning(options =>
    //        {
    //            options.DefaultApiVersion = new ApiVersion(2);
    //            options.ReportApiVersions = true;
    //            options.ApiVersionReader = new UrlSegmentApiVersionReader();
    //        }); 
    //    services.AddEndpointsApiExplorer();
    //}

    //private static void AddLogStack()
    //{
    //    Log.Logger = new LoggerConfiguration()
    //.Enrich.FromLogContext()
    //.WriteTo.Console()
    //.WriteTo.OpenSearch(new OpenSearchSinkOptions(new Uri("http://necotemplate-elasticsearch:9200"))
    //{
    //    AutoRegisterTemplate = true,
    //    IndexFormat = "myapp-logs-{0:yyyy.MM.dd}",
    //    NumberOfShards = 1,
    //    NumberOfReplicas = 0
    //})
    //.CreateLogger();
    //}
    private static void AddLogStack()
    {
        Log.Logger = new LoggerConfiguration()
       .Enrich.FromLogContext()
       .Enrich.WithEnvironmentName()
       .Enrich.WithThreadId()
       .WriteTo.Console()
       .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
           requestUri: "http://logstash:8080", // Ensure Logstash listens here
           textFormatter: new JsonFormatter())
       .CreateLogger();
    }

    //private static void AddMassTransit(this IServiceCollection services)
    //{
    //    // Register MassTransit with necessary configuration
    //    services.AddMassTransit(x =>
    //    {
    //        // Register request client for app2 (https://localhost:44387)
    //        x.AddRequestClient<ExampleAuthRequest>(new Uri("https://localhost:44387"));

    //        // Register the consumer for app2 (consumer to handle the request for app2)
    //        //x.AddConsumer<App2ExampleConsumer>();

    //        // Optional: Register additional consumers for app3 or others
    //        // x.AddConsumer<App3ExampleConsumer>();

    //        // Configure bus for gRPC communication with app2
    //        x.UsingGrpc((context, cfg) =>
    //        {
    //            cfg.Host(h =>
    //            {
    //                h.Host = "localhost";
    //                h.Port = 19796; // Custom gRPC port
    //            });

    //            // Optionally configure additional gRPC parameters like timeouts or retries
    //            cfg.ConfigureEndpoints(context);
    //        });
    //    });

    //    // Register the MassTransit hosted service to start the bus with the application lifecycle
    //    services.AddMassTransitHostedService();
    //}

}
