using WhereIsTheBus.TelegramBot.Extensions;

namespace WhereIsTheBus.TelegramBot.Services;

internal sealed class HttpScheduleClient : IScheduleClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<HttpScheduleClient> _logger;

    public HttpScheduleClient(HttpClient httpClient,
                              IConfiguration configuration,
                              ILogger<HttpScheduleClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IEnumerable<TransportStop>> StopsAsync(TransportRoute route)
    {
        var response = await _httpClient.PostAsJsonAsync(_configuration.ScheduleService(), route);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Schedule Service Request is OK");
            return await response.Content.ReadFromJsonAsync<IEnumerable<TransportStop>>()
                ?? Array.Empty<TransportStop>();
        }
        
        _logger.LogInformation("Schedule Service Request is NOT OK");
        return Array.Empty<TransportStop>();
    }
}