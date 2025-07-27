using MediatR;
using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
