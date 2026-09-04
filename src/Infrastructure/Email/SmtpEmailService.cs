using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using SpaceReservationSystem.API.Abstractions.Email;

namespace SpaceReservationSystem.Infrastructure.Mail;
// Implementación REAL de IEmailService: manda el correo de verdad 
public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _settings;

    // IOptions<SmtpSettings> es cómo .NET inyecta la configuración leída de appsettings
    public SmtpEmailService(IOptions<SmtpSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default)
    {
        // Cliente SMTP configurado con los datos de Gmail
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.Username, _settings.Password),
            EnableSsl = true // obligatorio para Gmail
        };

        // Arma el mensaje
        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = false // true si más adelante quieren correos con formato HTML
        };
        message.To.Add(toEmail);

        // Envío real, asíncrono, hacia el servidor SMTP
        await client.SendMailAsync(message, ct);
    }
}