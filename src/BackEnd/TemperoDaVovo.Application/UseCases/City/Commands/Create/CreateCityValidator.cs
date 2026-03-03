using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.City.Commands.Create;

public class CreateCityValidator : AbstractValidator<CreateCityRequestJson>
{
    public CreateCityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(1).WithMessage("Name must be greater than or equal to 1");
    }
}