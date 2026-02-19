using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Create;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequestJson>
{
    public CreateOrderValidator()
    {
        RuleFor(o => o.RestaurantId)
            .NotEmpty().WithMessage("Id do restaurante é obrigatório.");

        RuleFor(o => o.ClientSessionId)
            .NotEmpty().WithMessage("Id da sessão do cliente é obrigatório");

        RuleFor(o => o.CustomerName)
            .NotEmpty().WithMessage("O nome do cliente é obrigatório")
            .MinimumLength(2).WithMessage("O nome do cliente deve ter pelo menos 3 caracteres")
            .MaximumLength(50).WithMessage("O nome do cliente deve ter no máximo 50 caracteres");

        RuleFor(o => o.CustomerPhone)
            .NotEmpty().WithMessage("O número de telefone é obrigatório.");
        
        
    }
}