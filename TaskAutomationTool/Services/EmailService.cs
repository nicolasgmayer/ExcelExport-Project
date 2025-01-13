using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TaskAutomationTool.Services
{
    public class EmailService
    {
        public void SendEmail(IEnumerable<string> recipients, string subject, string body, string attachmentPath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            string senderEmail = configuration["EmailSettings:SenderEmail"];
            string senderPassword = configuration["EmailSettings:SenderPassword"];
            try
            {
                if (!recipients.Any())
                {
                    Console.WriteLine("No se encontraron direcciones de correo en la base de datos.");
                    return;
                }

                // Configuración del cliente SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword), 
                    EnableSsl = true,
                };

                // Configuración del correo
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false, // Cambiar a true si el cuerpo es HTML
                };

                // Agregar destinatarios
                foreach (var recipient in recipients)
                {
                    mailMessage.To.Add(recipient);
                }

                // Agregar archivo adjunto si se proporciona un path válido
                if (!string.IsNullOrEmpty(attachmentPath))
                {
                    var attachment = new Attachment(attachmentPath);
                    mailMessage.Attachments.Add(attachment);
                }

                // Enviar el correo electrónico
                smtpClient.Send(mailMessage);
                Console.WriteLine("Correo enviado con éxito a los destinatarios.");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }
        }
    }
}
