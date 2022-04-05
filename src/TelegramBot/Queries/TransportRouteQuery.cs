using WhereIsTheBus.TelegramBot.Attributes;

namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/bus", "/tram", "troll")]
internal sealed class TransportRouteQuery : FromMessageQuery<TransportRoute>
{
    public TransportRouteQuery(Message message) : base(message) => 
        Value = TransportRoute.Parse(message.Text!.Split());

    public override TransportRoute? Value { get; }
}