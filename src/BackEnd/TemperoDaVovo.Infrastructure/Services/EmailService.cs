using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces;

namespace TemperoDaVovo.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;
    private readonly string _fromEmail;

    public EmailService(IConfiguration configuration)
    {
        _smtpHost = configuration["Email:SmtpHost"]!;
        _smtpPort = int.Parse(configuration["Email:SmtpPort"]!);
        _smtpUser = configuration["Email:SmtpUser"]!;
        _smtpPass = configuration["Email:SmtpPass"]!;
        _fromEmail = configuration["Email:FromEmail"]!;
    }

    public async Task SendVerificationCodeAsync(string toEmail, string code, VerificationCodeType type)
    {
        var subject = type == VerificationCodeType.EmailConfirmation
            ? "Confirmação de cadastro"
            : "Código de autenticação";

        var body = type == VerificationCodeType.EmailConfirmation
            ? $"Seu código de confirmação de cadastro é: <strong>{code}</strong><br/>Válido por 10 minutos."
            : $"Seu código de autenticação é: <strong>{code}</strong><br/>Válido por 10 minutos.";

        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(_fromEmail, "Tempero Da Vovo"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(toEmail);

        await client.SendMailAsync(message);
    }
}