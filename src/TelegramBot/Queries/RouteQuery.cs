using WhereIsTheBus.TelegramBot.Attributes;

namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/bus", "/tram", "troll")]
internal sealed class TransportRouteQuery : FromArgsRequest<TransportRoute>
{
    public TransportRouteQuery(string[] args) : base(args) => 
        Value = TransportRoute.Parse(args);

    public override TransportRoute? Value { get; }
}