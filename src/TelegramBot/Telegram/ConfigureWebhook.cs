using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using WhereIsTheBus.TelegramBot.Configuration;

namespace WhereIsTheBus.TelegramBot.Telegram;

public class ConfigureWebhook : IHostedService
{
    private readonly BotConfiguration _configuration;
    private readonly IServiceProvider _services;
    private readonly ILogger<ConfigureWebhook> _logger;

    public ConfigureWebhook(IConfiguration configuration,
                            IServiceProvider services,
                            ILogger<ConfigureWebhook> logger)
    {
        _configuration = configuration.OfBot();
        _services = services;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        var webhookAddress = @$"{_configuration.HostAddress}/bot/{_configuration.Token}";
        _logger.LogInformation("Setting webhook: {webhookAddress}", webhookAddress);
        await client.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        _logger.LogInformation("Removing webhook");
        await client.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}