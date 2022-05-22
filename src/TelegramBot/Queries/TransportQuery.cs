namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("Автобусы", "Трамваи", "Троллейбусы")]
internal sealed class TransportQuery : FromUpdateQuery
{
    public TransportQuery(UpdateEvent update) : base(update)
    {
        string[] args = update.UserMessage.Split();

        Transport = args[0] switch
        {
            "Автобусы" => TransportType.Bus,
            "Трамваи" => TransportType.Tram,
            "Троллейбусы" => TransportType.Trolleybus,
            _ => throw new ArgumentOutOfRangeException(nameof(update.UserMessage), "Transport must be specified")
        };

        FullList = args.Length > 1 && args[1] == "все";
    }

    public TransportType? Transport { get; }

    public bool FullList { get; }
}