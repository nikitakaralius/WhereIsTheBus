namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/bus", "/tram", "/troll", "/b", "t", "/tr")]
internal sealed class TransportRouteQuery : FromUpdateQuery
{
    public TransportRouteQuery(UpdateEvent update) : base(update) => 
        Value = TransportRoute.Parse(update.UserMessage![1..].Split());

    public TransportRoute? Value { get; }
}