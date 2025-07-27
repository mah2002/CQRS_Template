using MediatR;
using Microsoft.Extensions.Logging;
using NecoTemplate.Application.Abstractions.Caching;
using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Application.Abstractions.Behaviors;

internal sealed class CacheBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<CacheBehavior<TRequest, TResponse>> _logger;

    public CacheBehavior(
        ICacheService cacheService,
        ILogger<CacheBehavior<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await _cacheService.GetAsync<TResponse>(
            request.CacheKey,
            cancellationToken);

        string name = typeof(TRequest).Name;
        if (cachedResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);

            return cachedResult;
        }

        _logger.LogInformation("Cache miss for {Query}", name);

        var result = await next();

        if (result.IsSuccess)
        {
            await _cacheService.SetAsync(request.CacheKey, result, request.Expiration, cancellationToken);
        }

        return result;
    }
}

