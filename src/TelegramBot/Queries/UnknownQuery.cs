namespace WhereIsTheBus.TelegramBot.Queries;

internal sealed class UnknownQuery : FromUpdateQuery
{
    public UnknownQuery(UpdateEvent update) : base(update)
    {
    }
}