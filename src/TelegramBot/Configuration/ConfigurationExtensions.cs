namespace WhereIsTheBus.TelegramBot.Configuration;

public static class ConfigurationExtensions
{
    public static string ScheduleService(this IConfiguration configuration) => 
        configuration["ScheduleService"];
}