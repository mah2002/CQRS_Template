using System.Data.Common;

namespace NecoTemplate.Application.Abstractions.Database;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
