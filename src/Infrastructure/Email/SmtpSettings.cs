namespace SpaceReservationSystem.Infrastructure.Mail;

// Representa la sección "Smtp" de appsettings.Development.json.
// .NET la llena automáticamente con Configure<SmtpSettings>(...) más adelante.
public class SmtpSettings
{
    public string Host { get; set; } = null!;      // ej: smtp.gmail.com
    public int Port { get; set; }                  
    public string Username { get; set; } = null!;   // tu correo de envío
    public string Password { get; set; } = null!;   // contraseña de aplicación
    public string FromEmail { get; set; } = null!;   // correo que aparece como remitente
    public string FromName { get; set; } = null!;    // nombre que aparece como remitente
}