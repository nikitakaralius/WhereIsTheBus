using System.Text.Json;

namespace WhereIsTheBus.TelegramBot.Services;

internal class HttpScheduleClient : IScheduleClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpScheduleClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Stop>> StopsAsync(TransportRoute route)
    {
        var response = await _httpClient.PostAsJsonAsync(_configuration.ScheduleService(), route);

        if (response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Stop>>(
                await response.Content.ReadAsStreamAsync()) ?? Array.Empty<Stop>();
        }
        return Array.Empty<Stop>();
    }
}