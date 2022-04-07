namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("Автобусы", "Трамваи", "Троллейбусы")]
public class TransportQuery : FromUpdateQuery
{
    public TransportQuery(UpdateEvent update) : base(update)
    {
        
    }
}