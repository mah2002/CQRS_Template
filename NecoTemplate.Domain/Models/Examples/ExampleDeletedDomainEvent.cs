using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Domain.Models.Examples;

internal class ExampleDeletedDomainEvent(
    Guid exampleId,
    string exampleTitle) : IDomainEvent
{
    public Guid ExampleIdProperty { get; init; } = exampleId;
}
