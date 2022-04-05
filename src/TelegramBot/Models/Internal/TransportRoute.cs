namespace WhereIsTheBus.TelegramBot.Models.Internal;

internal record TransportRoute(Transport Transport, int Number, Direction Direction)
{
    public static TransportRoute? Parse(string[] args)
    {
        if (args.Length < 3)
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

        Direction? direction = args[2] switch
        {
            "0" or "d" or "direct" => Direction.Direct,
            "1" or "r" or "return" => Direction.Return,
            _                      => null
        };

        if (transport is null || direction is null)
        {
            return null;
        }

        return new TransportRoute(transport.Value, number, direction.Value);
    }
}

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