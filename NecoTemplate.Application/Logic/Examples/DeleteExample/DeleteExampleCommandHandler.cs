using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Domain.Models.Examples;
using NecoTemplate.Domain.Models.Examplesl;

namespace NecoTemplate.Application.Logic.Examples.DeleteExample;

internal sealed class DeleteExampleCommandHandler(
    IExampleRepository _exampleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteExampleCommand>
{
    public async Task<Result> Handle(DeleteExampleCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = _exampleRepository.Delete(request.ExampleId);
        if (isDeleted == false)
        {
            return Result.Failure(ExampleErrors.NotFound(request.ExampleId, "DeleteExample.NotFound.02"));
        }

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Result.Success();
    }
}
