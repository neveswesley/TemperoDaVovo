using System.Data;
using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.SideDishGroup.Commands;

public class CreateSideDishGroupValidator : AbstractValidator<CreateSideDishGroupRequestJson>
{
    public CreateSideDishGroupValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MinimumLength(2).WithMessage("O nome deve conter pelo menos 2 caracteres.")
            .MaximumLength(150).WithMessage("O nome não pode conter mais de 150 caracteres.");
        
        RuleFor(s=>s.MaxQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade máxima não pode ser menor do que 0.");

        RuleFor(s => s.MinQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade mínima não pode ser menor do que 0.");

        RuleFor(s => s.RestaurantId)
            .NotEmpty().WithMessage("O Id do restaurante deve ser preenchido.");

    }
}