using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Create;

public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantRequestJson>
{
    public CreateRestaurantValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("O nome do restaurante não pode ser vazio.")
            .MinimumLength(2).WithMessage("O nome do restaurante precisa ter pelo menos 2 caracteres.")
            .MaximumLength(50).WithMessage("O nome do restaurante não pode ter mais de 50 caracteres");

        RuleFor(r => r.Phone)
            .NotEmpty().WithMessage("O número de telefone não pode ser vazio.");

        RuleFor(r => r.Address)
            .NotEmpty().WithMessage("O endereço não pode ser vazio.");
    }
}