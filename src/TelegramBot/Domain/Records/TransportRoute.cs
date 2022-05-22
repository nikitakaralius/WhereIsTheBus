namespace WhereIsTheBus.TelegramBot.Domain.Records;

public record TransportRoute(TransportType Transport, int Number, Direction Direction)
{
    public static TransportRoute? Parse(string[] args)
    {
        if (args.Length < 2)
        {
            return null;
        }

        TransportType? transport = args[0] switch
        {
            "bus" or "b"    => TransportType.Bus,
            "tram" or "t"   => TransportType.Tram,
            "troll" or "tr" => TransportType.Trolleybus,
            _               => null
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
                "b" or _               => Direction.Both
            }
            : Direction.Both;

        return transport is null
            ? null
            : new TransportRoute(transport.Value, number, direction);
    }
}