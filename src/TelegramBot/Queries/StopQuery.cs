namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/s", "/stop")]
public class StopQuery : FromUpdateQuery
{
    public StopQuery(UpdateEvent update) : base(update)
    {
        string[] args = update.UserMessage.Split();
        if (args.Length < 2)
        {
            return;
        }

        if (int.TryParse(args[1], out int value))
        {
            StopId = value;
        }
    }

    public int? StopId { get; }
}