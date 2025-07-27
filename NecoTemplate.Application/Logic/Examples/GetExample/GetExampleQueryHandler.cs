using System.Data.Common;
using Dapper;
using MassTransit;
using MassTransit.Clients;
using NecoTemplate.Application.Abstractions.Caching;
using NecoTemplate.Application.Abstractions.Database;
using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Domain.Models.Examples;
using NecoTemplate.Domain.Models.Examplesl;
using NecoTemplate.Domain.Responses;
using NecoTemplate.gRPC;
using static Dapper.SqlMapper;

namespace NecoTemplate.Application.Logic.Examples.GetExample;

internal sealed class GetExamplesQueryHandler(
    IExampleRepository _exampleRepository,
    ICacheService cacheService,
    IAuthService authService) : IQueryHandler<GetExampleQuery, ExampleResponse>
{
    public async Task<Result<ExampleResponse>> Handle(GetExampleQuery request, CancellationToken cancellationToken)
    {
        //var isAuthenticated = await authService.GetAuthAsync(request.ExampleId);
        //if (isAuthenticated==null)
        //{
        //    return Result.Failure<ExampleResponse>(ExampleErrors.CanNotGetResponse("ExampleError.CanNotGetResponse.nAuth"));
        //}
        bool isAuthenticated = true; //get from auth app
        if (isAuthenticated == true)
        {
            var cacheKey = $"example-{request.ExampleId}";
            var cachedExample = await cacheService.GetAsync<ExampleResponse>(cacheKey);

            if (cachedExample is not null)
            {
                return cachedExample;
            }

            var example = await _exampleRepository.GetByIdAsync(request.ExampleId, cancellationToken).ConfigureAwait(false);

            if (example is null)
            {
                return Result.Failure<ExampleResponse>(ExampleErrors.NotFound(request.ExampleId, "ExampleError.NotFound.03"));
            }

            var response = new ExampleResponse()
            {
                Id = example.Id,
                Name = example.ExampleNameProperty
            };
            await cacheService.SetAsync(cacheKey, response);
            return response;
        }
        else
        {
            return Result.Failure<ExampleResponse>(ExampleErrors.NotAuthorized("ExampleError.NotFound.06"));
        }
    }
}
