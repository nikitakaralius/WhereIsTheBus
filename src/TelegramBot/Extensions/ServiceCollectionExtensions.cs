using System.Reflection;
using WhereIsTheBus.TelegramBot.Telegram.RequestRouter;

namespace WhereIsTheBus.TelegramBot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTelegramRouter(this IServiceCollection services)
    {
        return services.AddSingleton<ITelegramRequestRouter, TelegramRequestRouter>(
            _ => TelegramRequestRouter
                .InitializeFromAssemblyTypes(Assembly.GetExecutingAssembly()));
    }
}