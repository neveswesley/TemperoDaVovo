using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.SideDishGroup.Commands.CreateSideDish;

public class CreateSideDishValidator : AbstractValidator<CreateSideDishRequestJson>
{
    public CreateSideDishValidator()
    {
        RuleFor(x => x.SideDishGroupId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("A quantidade deve ser maior que 0.");
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("O preço deve ser maior que 0");
    }
}