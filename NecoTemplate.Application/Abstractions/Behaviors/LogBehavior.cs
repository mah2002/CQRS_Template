using MediatR;
using Microsoft.Extensions.Logging;
using NecoTemplate.Domain.Abstractions;
using Serilog.Context;

namespace NecoTemplate.Application.Abstractions.Behaviors;

public class LogBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{
    private readonly ILogger<LogBehavior<TRequest, TResponse>> _logger;

    public LogBehavior(ILogger<LogBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Executing request {requestName}", requestName);

            var result = await next();

            if (result.IsSuccess)
            {
                _logger.LogInformation($"Request {requestName} processed successfully", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    _logger.LogError($"Request {requestName} processed with error", requestName);
                }
            }

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Request {RequestName} processing failed", requestName);

            throw;
        }
    }
}
