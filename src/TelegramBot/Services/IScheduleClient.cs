namespace WhereIsTheBus.TelegramBot.Services;

internal interface IScheduleClient
{
    Task<IEnumerable<Stop>> StopsAsync(TransportRoute route);
}