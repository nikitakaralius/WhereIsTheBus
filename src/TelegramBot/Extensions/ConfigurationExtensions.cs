namespace WhereIsTheBus.TelegramBot.Extensions;

internal static class ConfigurationExtensions
{
    public static string ScheduleService(this IConfiguration configuration) => 
        configuration["ScheduleService"];

    public static BotConfiguration OfBot(this IConfiguration configuration) =>
        configuration
            .GetSection(nameof(BotConfiguration))
            .Get<BotConfiguration>();
}