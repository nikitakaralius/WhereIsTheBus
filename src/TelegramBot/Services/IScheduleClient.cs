namespace WhereIsTheBus.TelegramBot.Services;

public interface IScheduleClient
{
    Task<IEnumerable<TransportStop>> StopsAsync(TransportRoute route);
}