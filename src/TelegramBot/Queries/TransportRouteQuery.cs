using WhereIsTheBus.TelegramBot.Attributes;

namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/bus", "/tram", "/troll", "/b", "t", "/tr")]
internal sealed class TransportRouteQuery : FromMessageQuery<TransportRoute>
{
    public TransportRouteQuery(Message message) : base(message) => 
        Value = TransportRoute.Parse(message.Text![1..].Split());

    public override TransportRoute? Value { get; }
}