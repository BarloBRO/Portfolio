using System.ComponentModel.DataAnnotations;
using BarloPortfolio.Server.Models;
using BarloPortfolio.Server.Options;
using BarloPortfolio.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.SectionName));
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();

var clientOrigins = builder.Configuration.GetSection("ClientOrigins").Get<string[]>()
    ?? ["http://localhost:5162", "https://localhost:7220"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("PortfolioClient", policy =>
    {
        policy.WithOrigins(clientOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("PortfolioClient");

app.MapGet("/", () => Results.Ok(new { status = "ok", service = "BarloPortfolio.Server" }));

app.MapPost("/api/contact", async (ContactRequest request, IEmailSender emailSender, ILogger<Program> logger, CancellationToken cancellationToken) =>
{
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, validateAllProperties: true);
    if (!isValid)
    {
        var errors = validationResults
            .SelectMany(r => r.MemberNames.DefaultIfEmpty(string.Empty).Select(m => (Member: m, r.ErrorMessage)))
            .GroupBy(x => x.Member)
            .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage ?? "Invalid value").ToArray());

        return Results.ValidationProblem(errors);
    }

    try
    {
        await emailSender.SendContactMessageAsync(request, cancellationToken);
        return Results.Ok(new { message = "Message sent. Thanks for reaching out!" });
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to send contact email from {Email}", request.Email);
        return Results.Problem("Could not send your message right now. Please try again later or email directly.", statusCode: 502);
    }
});

app.Run();
