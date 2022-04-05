namespace WhereIsTheBus.TelegramBot.Models.Internal;

internal record TransportRoute(Transport Transport, int Number, Direction Direction)
{
    public static TransportRoute? Parse(string[] args)
    {
        if (args.Length < 2)
        {
            return null;
        }
        
        Transport? transport = args[0] switch
        {
            "/bus"   => Transport.Bus,
            "/tram"  => Transport.Tram,
            "/troll" => Transport.Trolleybus,
            _        => null
        };

        if (int.TryParse(args[1], out int number) == false)
        {
            return null;
        }

        Direction direction = args.Length >= 3 ? args[2] switch
        {
            "0" or "d" or "direct" => Direction.Direct,
            "1" or "r" or "return" => Direction.Return,
            _                      => Direction.Both
        } : Direction.Both;

        return transport is null
            ? null
            : new TransportRoute(transport.Value, number, direction);
    }
}

internal enum Direction
{
    Direct = 0,
    Return = 1,
    Both = 2
}

internal enum Transport
{
    Bus = 0,
    Trolleybus = 1,
    Tram = 2
}