using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Telegram;

internal interface ITelegramRouter
{
    FromArgsQuery? QueryBy(string[] args);
}