namespace WhereIsTheBus.TelegramBot.Queries;

[TelegramRoutes("/s", "/stop")]
public class StopQuery : FromUpdateQuery<int?>
{
    public StopQuery(UpdateEvent update) : base(update)
    {
        if (update.UserMessage.Length < 2)
        {
            return;
        }

        if (int.TryParse(update.UserMessage, out int value))
        {
            Value = value;
        }
    }

    public override int? Value { get; }
}