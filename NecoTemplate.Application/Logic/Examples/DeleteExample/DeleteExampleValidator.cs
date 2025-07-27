using FluentValidation;

namespace NecoTemplate.Application.Logic.Examples.DeleteExample;

internal sealed class DeleteExampleValidator: AbstractValidator<DeleteExampleCommand>
{
    public DeleteExampleValidator()
    {
        RuleFor(c => c.ExampleId).NotEmpty().WithMessage("Identifier is not provided.");
    }
}
