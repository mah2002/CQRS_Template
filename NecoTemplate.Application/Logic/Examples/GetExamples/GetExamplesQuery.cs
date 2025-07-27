using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Responses;

namespace NecoTemplate.Application.Logic.Examples.GetExamples;

public sealed record GetExamplesQuery() : IQuery<IEnumerable<ExampleResponse>>;
