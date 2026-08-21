using BarloPortfolio.Server.Models;

namespace BarloPortfolio.Server.Services;

public interface IEmailSender
{
    Task SendContactMessageAsync(ContactRequest request, CancellationToken cancellationToken);
}
