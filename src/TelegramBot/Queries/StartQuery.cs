namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/start")]
public class StartQuery : FromUpdateQuery
{
    public StartQuery(UpdateEvent update) : base(update) { }

}