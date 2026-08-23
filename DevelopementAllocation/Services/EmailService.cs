using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using DevelopementAllocation.Models;

namespace DevelopementAllocation.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendBookingConfirmationAsync(ShuttleSlotEntry entry, DateTime bookingDate)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(new MailboxAddress(entry.Name, entry.MailId));
            message.Subject = "Shuttle Slot Booking Confirmed";

            message.Body = new TextPart("html")
            {
                Text = $@"
                    <div style='font-family:sans-serif;padding:20px;'>
                        <h2 style='color:#3b82f6;'>Booking Confirmed 🎉</h2>
                        <p>Hi {entry.Name},</p>
                        <p>Your shuttle slot has been successfully booked.</p>
                        <table style='margin-top:10px;'>
                            <tr><td><b>Date:</b></td><td>{bookingDate:dd MMM yyyy}</td></tr>
                            <tr><td><b>Slot:</b></td><td>{entry.Slot}</td></tr>
                            <tr><td><b>Mobile:</b></td><td>{entry.MobileNumber}</td></tr>
                        </table>
                        <p style='margin-top:20px;color:#64748b;font-size:13px;'>
                            If you did not make this booking, please contact support.
                        </p>
                    </div>"
            };

            using var client = new SmtpClient();
            client.CheckCertificateRevocation = false;
            await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}