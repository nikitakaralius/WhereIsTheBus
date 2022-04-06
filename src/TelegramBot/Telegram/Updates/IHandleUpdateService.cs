namespace WhereIsTheBus.TelegramBot.Telegram.Updates;

public interface IHandleUpdateService
{
    Task EchoAsync(Update update);
}