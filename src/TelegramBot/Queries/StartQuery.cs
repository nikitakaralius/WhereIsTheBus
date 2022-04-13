namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/start")]
internal sealed class StartQuery : FromUpdateQuery
{
    public StartQuery(UpdateEvent update) : base(update) { }

}