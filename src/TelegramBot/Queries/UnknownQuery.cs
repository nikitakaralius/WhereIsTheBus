namespace WhereIsTheBus.TelegramBot.Queries;

internal class UnknownQuery : FromUpdateQuery
{
    public UnknownQuery(UpdateEvent update) : base(update)
    {
    }
}