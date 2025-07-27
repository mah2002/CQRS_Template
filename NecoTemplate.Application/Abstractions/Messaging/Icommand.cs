using MediatR;
using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand;

public interface IBaseCommand;
