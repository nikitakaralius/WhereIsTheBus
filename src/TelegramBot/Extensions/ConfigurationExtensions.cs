namespace WhereIsTheBus.TelegramBot.Extensions;

public static class ConfigurationExtensions
{
    public static string Token(this IConfiguration configuration) => configuration["Token"];

    public static string ScheduleService(this IConfiguration configuration) =>
        configuration["ScheduleService"];
}