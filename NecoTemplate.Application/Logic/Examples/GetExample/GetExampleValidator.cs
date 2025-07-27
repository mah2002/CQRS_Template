using FluentValidation;

namespace NecoTemplate.Application.Logic.Examples.GetExample;

internal sealed class GetExamplesValidator: AbstractValidator<GetExampleQuery>
{
    public GetExamplesValidator()
    {
        RuleFor(c => c.ExampleId).NotEmpty().WithMessage("Identifier is not provided.");
    }
}
