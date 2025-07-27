using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Responses;

namespace NecoTemplate.Application.Logic.Examples.GetExample;

public sealed record GetExampleQuery(Guid ExampleId) : IQuery<ExampleResponse>;
