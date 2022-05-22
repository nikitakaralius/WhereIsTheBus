namespace WhereIsTheBus.TelegramBot.Services;

internal interface IScheduleClient
{
    Task<IEnumerable<TransportStop>> StopsAsync(TransportRoute route);

    Task<IEnumerable<StopArrivals>> TransportAsync(int stopId);
}