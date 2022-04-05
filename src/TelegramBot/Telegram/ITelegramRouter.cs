using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Telegram;

internal interface ITelegramRouter
{
    FromMessageQuery? QueryFrom(Message message);
}