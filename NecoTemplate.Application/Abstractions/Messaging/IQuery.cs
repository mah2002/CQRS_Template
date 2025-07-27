using MediatR;
using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;