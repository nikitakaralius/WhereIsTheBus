namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("Автобусы", "Трамваи", "Троллейбусы")]
public class TransportQuery : FromUpdateQuery
{
    public TransportQuery(UpdateEvent update) : base(update)
    {
        string[] args = update.UserMessage.Split();

        if (args.Length < 2)
        {
            return;
        }

        Transport = args[1] switch
        {
            "Автобусы" => TransportType.Bus,
            "Трамваи" => TransportType.Tram,
            "Троллейбусы" => TransportType.Trolleybus,
            _ => throw new ArgumentOutOfRangeException(nameof(update.UserMessage), "Transport must be specified")
        };
    }

    public TransportType? Transport { get; }
}