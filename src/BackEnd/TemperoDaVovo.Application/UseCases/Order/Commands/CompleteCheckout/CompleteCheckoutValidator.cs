using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CompleteCheckout;

public class CompleteCheckoutValidator : AbstractValidator<CompleteCheckoutRequestJson>
{
    public CompleteCheckoutValidator()
    {
        RuleFor(request => request.Phone)
            .NotEmpty().WithMessage("Phone cannot be empty");
        
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage("Name cannot be empty");
    }
}