using FluentValidation;
using NecoTemplate.Application.Logic.Examples.GetExample;

namespace NecoTemplate.Application.Logic.Examples.GetExamples;

internal sealed class GetExamplesValidator: AbstractValidator<GetExampleQuery>
{
    public GetExamplesValidator()
    {
        RuleFor(c => c.ExampleId).NotEmpty().WithMessage("Identifier is not provided.");
    }
}
