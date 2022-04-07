namespace WhereIsTheBus.TelegramBot.Queries;

public sealed class UnknownQuery : FromUpdateQuery
{
    public UnknownQuery(UpdateEvent update) : base(update) { }
}