using Microsoft.Extensions.Configuration;
using NecoTemplate.Application.Abstractions.Database;
using Npgsql;
using System.Data.Common;

namespace NecoTemplate.Infrastructure.Database;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource,
    IConfiguration configuration) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
