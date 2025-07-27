using Microsoft.EntityFrameworkCore;
using NecoTemplate.Application.Abstractions.Exceptions;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Infrastructure.Database;

namespace NecoTemplate.Infrastructure.Repositories;

public abstract class Repository<T>
    where T : EntityBase
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    public virtual void Add(T entity)
    {
        try
        {
            DbContext.Add(entity);
        }
        catch (DbUpdateException exception)
        {
            throw new DbUpdateException("Add exception occurred.", exception);
        }
    }
}
