using FluentValidation;

namespace NecoTemplate.Application.Logic.Examples.CreateExample;

internal sealed class CreateExampleValidator: AbstractValidator<CreateExampleCommand>
{
    public CreateExampleValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Example must have name.");
    }
}
