using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Domain.Models.Examples;
using NecoTemplate.Domain.Models.Examplesl;

namespace NecoTemplate.Application.Logic.Examples.CreateExample;

internal sealed class CreateExampleCommandHandler(
    IExampleRepository _exampleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateExampleCommand,Guid>
{
    public async Task<Result<Guid>> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
    {
        // get from  client or create id?
        var example = Example.Create(new Guid(), request.Name);

        _exampleRepository.Add(example);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);



        return Result.Success(example.Id);
    }
}
