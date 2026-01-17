using FluentValidation;
using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.User.Create;

public class CreateUserValidator : AbstractValidator<CreateUserRequestJson>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.RestaurantId).NotEmpty();
        
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Endereço de e-mail obrigatório.")
            .EmailAddress().WithMessage("Endereço de e-mail inválido.");
        
        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Senha obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve conter pelo menos 6 caracteres.");
    }
}