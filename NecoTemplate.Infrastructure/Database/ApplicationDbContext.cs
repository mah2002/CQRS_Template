using System.Data;
using System.Reflection.Emit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NecoTemplate.Application.Abstractions.Clock;
using NecoTemplate.Application.Abstractions.Exceptions;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Domain.Models.Examples;

namespace NecoTemplate.Infrastructure.Database;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{

    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IPublisher _publisher;

    public DbSet<Example> Examples { get; set; }

    public ApplicationDbContext(
        DbContextOptions options,
        IDateTimeProvider dateTimeProvider,
        IPublisher publisher)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
        _publisher=publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var domainEvents = AddDomainEvents();

            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private async Task AddDomainEvents()
    {
        var domainEvents = ChangeTracker
            .Entries<EntityBase>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            }).ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
