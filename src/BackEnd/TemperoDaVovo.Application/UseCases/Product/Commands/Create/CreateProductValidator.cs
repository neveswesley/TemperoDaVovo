using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Create;

public class CreateProductValidator : AbstractValidator<CreateProductRequestJson>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Por favor, informe o nome do produto.")
            .MaximumLength(100).WithMessage("O nome do produto não pode conter mais de 100 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("A descrição não pode conter mais de 200 caracteres.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Por favor, insira o preço do produto.")
            .GreaterThanOrEqualTo(0).WithMessage("Preço inválido.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Por favor, selecione uma categoria.")
            .IsInEnum().WithMessage("Categoria inválida.");

    }
}