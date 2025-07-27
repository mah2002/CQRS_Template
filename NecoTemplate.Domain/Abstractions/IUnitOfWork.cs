using Microsoft.EntityFrameworkCore;
using NecoTemplate.Domain.Models.Examples;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<Example> Examples { get; }

}