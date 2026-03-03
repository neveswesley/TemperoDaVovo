using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Create;

public class CreateNeighborhoodValidator : AbstractValidator<CreateNeighborhoodRequestJson>
{
    public CreateNeighborhoodValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Name cannot exceed 50 characters");
        RuleFor(x => x.DeliveryFee).GreaterThanOrEqualTo(0).WithMessage("Fee must be greater than 0");
    }
}