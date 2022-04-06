namespace WhereIsTheBus.TelegramBot.Services;

internal interface IScheduleClient
{
    Task<IEnumerable<TransportStop>> StopsAsync(TransportRoute route);
}