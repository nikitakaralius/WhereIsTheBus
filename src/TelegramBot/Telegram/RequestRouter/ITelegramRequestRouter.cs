namespace WhereIsTheBus.TelegramBot.Telegram.RequestRouter;

internal interface ITelegramRequestRouter
{
    IRequest RequestFrom(Message message);
}