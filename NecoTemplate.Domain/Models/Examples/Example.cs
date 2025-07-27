using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Domain.Models.Examples;

public sealed class Example : EntityBase
{
    public string ExampleNameProperty { get; private set; }

    public static Example Create(
        Guid id,
        string name)
    {
        var example = new Example
        {
            Id = id,
            ExampleNameProperty = name
        };

        example.Raise(new ExampleCreatedDomainEvent(
            example.Id,
            example.ExampleNameProperty));

        return example;
    }

    //optional
    public void Update(
        Guid id,
        string name)
    {
        Id = id;
        ExampleNameProperty = name;
    }

}
