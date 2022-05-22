namespace WhereIsTheBus.TelegramBot.Telegram.RequestRouter;

internal interface ITelegramRequestRouter
{
    IRequest RequestFrom(UpdateEvent update);
}