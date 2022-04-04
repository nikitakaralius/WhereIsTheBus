namespace WhereIsTheBus.TelegramBot.Models.Internal;

internal record TransportRoute(Transport Transport, int Number, Direction Direction);

internal enum Direction
{
    Direct = 0,
    Return = 1
}

internal enum Transport
{
    Bus = 0,
    Trolleybus = 1,
    Tram = 2
}