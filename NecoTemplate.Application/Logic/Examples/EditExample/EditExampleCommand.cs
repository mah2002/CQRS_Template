using NecoTemplate.Application.Abstractions.Messaging;

namespace NecoTemplate.Application.Logic.Examples.EditExample;

public sealed record EditExampleCommand(Guid ExampleId, string Name) : ICommand;
