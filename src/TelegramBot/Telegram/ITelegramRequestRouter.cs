namespace WhereIsTheBus.TelegramBot.Telegram;

internal interface ITelegramRequestRouter
{
    IRequest? RequestFrom(Message message);
}