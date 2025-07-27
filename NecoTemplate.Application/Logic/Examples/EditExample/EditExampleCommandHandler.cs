using NecoTemplate.Application.Abstractions.Messaging;
using NecoTemplate.Domain.Abstractions;
using NecoTemplate.Domain.Models.Examples;
using NecoTemplate.Domain.Models.Examplesl;

namespace NecoTemplate.Application.Logic.Examples.EditExample;

internal sealed class GetExampleQueryHandler(
    IExampleRepository _exampleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<EditExampleCommand>
{
    public async Task<Result> Handle(EditExampleCommand request, CancellationToken cancellationToken)
    {
        var example = await _exampleRepository.GetByIdAsync(request.ExampleId, cancellationToken).ConfigureAwait(false);
        if (example is null)
        {
            return Result.Failure(ExampleErrors.NotFound(request.ExampleId, "UpdateExample.NotFound.01"));
        }
        example.Update(request.ExampleId,request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Result.Success();
    }
}
