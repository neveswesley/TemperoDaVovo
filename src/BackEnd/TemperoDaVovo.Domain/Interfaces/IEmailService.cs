using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces;

public interface IEmailService
{
    Task SendVerificationCodeAsync(string toEmail, string code, VerificationCodeType type);
}