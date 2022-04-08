namespace WhereIsTheBus.TelegramBot.Extensions;

internal static class ConfigurationExtensions
{
    public static string ScheduleService(this IConfiguration configuration) => 
        configuration["ScheduleService"];

    public static string ScheduleServiceStops(this IConfiguration configuration) => 
        $"{configuration.ScheduleService()}/stops";

    public static string ScheduleServiceTransport(this IConfiguration configuration) => 
        $"{configuration.ScheduleService()}/transport";

    public static BotConfiguration OfBot(this IConfiguration configuration) =>
        configuration
            .GetSection(nameof(BotConfiguration))
            .Get<BotConfiguration>();
}