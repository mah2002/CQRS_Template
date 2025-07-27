using NecoTemplate.Application.Abstractions.Messaging;

namespace NecoTemplate.Application.Logic.Examples.CreateExample;

public sealed record CreateExampleCommand(string Name) : ICommand<Guid>;
