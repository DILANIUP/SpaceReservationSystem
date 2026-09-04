namespace SpaceReservationSystem.API.Abstractions.Email;

// Contrato para enviar correos 
public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body, CancellationToken ct = default);
}