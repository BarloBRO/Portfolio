namespace BarloPortfolio.Services;

public class ApiClient(HttpClient http)
{
    public HttpClient Http { get; } = http;
}
