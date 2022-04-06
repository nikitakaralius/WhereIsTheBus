namespace WhereIsTheBus.TelegramBot.Telegram.Updates;

public sealed record UpdateEvent(ChatId ChatId, string UserMessage)
{
    public static UpdateEvent FromMessage(Message message)
    {
        if (message.Text is null)
        {
            throw new ArgumentException("Message Text can not be null");
        }
        return new UpdateEvent(message.Chat.Id, message.Text);
    }

    public static UpdateEvent FromCallbackQuery(CallbackQuery query)
    {
        if (query.Data is null)
        {
            throw new ArgumentException("Query Data can not be null");
        }
        return new UpdateEvent(query.Message!.Chat.Id, query.Data);
    }
}