using NecoTemplate.Application.Abstractions.Messaging;

namespace NecoTemplate.Application.Logic.Examples.DeleteExample;

public sealed record DeleteExampleCommand(Guid ExampleId) : ICommand;
