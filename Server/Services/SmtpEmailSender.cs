using System.Net;
using System.Net.Mail;
using BarloPortfolio.Server.Models;
using BarloPortfolio.Server.Options;
using Microsoft.Extensions.Options;

namespace BarloPortfolio.Server.Services;

public class SmtpEmailSender(IOptions<SmtpOptions> options, ILogger<SmtpEmailSender> logger) : IEmailSender
{
    private readonly SmtpOptions _options = options.Value;

    public async Task SendContactMessageAsync(ContactRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.Host) || string.IsNullOrWhiteSpace(_options.ToAddress))
        {
            logger.LogWarning("SMTP is not configured. Set the Smtp:* settings (e.g. via dotnet user-secrets) to enable email delivery.");
            throw new InvalidOperationException("Email delivery is not configured on the server.");
        }

        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSsl,
            Credentials = new NetworkCredential(_options.Username, _options.Password),
        };

        using var mail = new MailMessage
        {
            From = new MailAddress(_options.FromAddress, _options.FromName),
            Subject = $"Portfolio contact from {request.Name}",
            Body = $"From: {request.Name} <{request.Email}>\n\n{request.Message}",
            IsBodyHtml = false,
        };
        mail.To.Add(_options.ToAddress);
        mail.ReplyToList.Add(new MailAddress(request.Email, request.Name));

        await client.SendMailAsync(mail, cancellationToken);
    }
}
