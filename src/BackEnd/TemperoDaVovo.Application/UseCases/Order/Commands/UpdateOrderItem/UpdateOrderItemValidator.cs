using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;

public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemRequest>
{
    public UpdateOrderItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be at least 1.");

        RuleForEach(x => x.SideDishes).ChildRules(sd =>
        {
            sd.RuleFor(x => x.SideDishId)
                .NotEmpty().WithMessage("SideDishId is required.");
            sd.RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Side dish quantity must be at least 1.");
        });
    }
}