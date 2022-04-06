namespace WhereIsTheBus.Domain.Records;

public record TransportRoute(Transport Transport, int Number, Direction Direction)
{
    public static TransportRoute? Parse(string[] args)
    {
        if (args.Length < 2)
        {
            return null;
        }

        Transport? transport = args[0] switch
        {
            "/bus" or "/b"    => Transport.Bus,
            "/tram" or "/t"   => Transport.Tram,
            "/troll" or "/tr" => Transport.Trolleybus,
            _                 => null
        };

        if (int.TryParse(args[1], out int number) == false)
        {
            return null;
        }

        if (number <= 0)
        {
            return null;
        }

        Direction direction = args.Length >= 3
            ? args[2] switch
            {
                "1" or "d" or "direct" => Direction.Direct,
                "2" or "r" or "return" => Direction.Return,
                _                      => Direction.Both
            }
            : Direction.Both;

        return transport is null
            ? null
            : new TransportRoute(transport.Value, number, direction);
    }
}