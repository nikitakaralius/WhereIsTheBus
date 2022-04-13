using WhereIsTheBus.TelegramBot.Extensions;

namespace WhereIsTheBus.TelegramBot;

internal static class DependencyInjection
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection services,
                                                 string clientName,
                                                 BotConfiguration botConfiguration)
    {
        services.AddMediatR(typeof(Program));
        services.AddHostedService<ConfigureWebhook>();
        services.AddHttpClient(clientName)
               .AddTypedClient<ITelegramBotClient>(
                   httpClient => new TelegramBotClient(botConfiguration.Token, httpClient));
        services.AddHttpClient<IScheduleClient, HttpScheduleClient>();
        services.AddScoped<IHandleUpdateService, HandleUpdateService>();
        services.AddTelegramRouter();
        services.AddControllers()
               .AddNewtonsoftJson();
        
        return services;
    }
}