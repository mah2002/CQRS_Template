using FluentValidation;

namespace NecoTemplate.Application.Logic.Examples.EditExample;

internal sealed class GetExapleValidator: AbstractValidator<EditExampleCommand>
{
    public GetExapleValidator()
    {
        RuleFor(c => c.ExampleId).NotEmpty().WithMessage("Identifier is not provided.");
        RuleFor(c => c.Name).NotEmpty().WithMessage("There is only name to edit. name must be filled.");
    }
}
