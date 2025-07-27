using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Domain.Models.Examples;

internal class ExampleEditedDomainEvent
(
    Guid exampleId,
    string exampleTitle) : IDomainEvent
{
    public Guid ExampleIdProperty { get; init; } = exampleId;

    public string ExampleNameProperty { get; init; } = exampleTitle;
}
