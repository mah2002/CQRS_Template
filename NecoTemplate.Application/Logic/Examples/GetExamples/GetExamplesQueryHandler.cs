using System.Data.Common;
using Dapper;
using NecoTemplate.Application.Abstractions.Caching;
using NecoTemplate.Application.Abstractions.Database;
using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Domain.Responses;

namespace NecoTemplate.Application.Logic.Examples.GetExamples;

internal sealed class GetExamplesQueryHandler(
        IDbConnectionFactory dbConnectionFactory,
        ICacheService cacheService) : IQueryHandler<GetExamplesQuery, IEnumerable<ExampleResponse>>
{
    public async Task<Result<IEnumerable<ExampleResponse>>> Handle(GetExamplesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = "all-examples";
        var cachedExamples = await cacheService.GetAsync<IEnumerable<ExampleResponse>>(cacheKey);

        if (cachedExamples is not null)
        {
            return Result.Success(cachedExamples);
        }
        using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync().ConfigureAwait(false);
        const string sql =
            $"""
              SELECT
              "Id" AS {nameof(ExampleResponse.Id)},
              "ExampleNameProperty"  AS {nameof(ExampleResponse.Name)}
              FROM public.example
             """;

        IEnumerable<ExampleResponse> examples = await connection.QueryAsync<ExampleResponse>(sql, request).ConfigureAwait(false);

        await cacheService.SetAsync(cacheKey, examples);
        return Result.Success(examples);
    }
}
