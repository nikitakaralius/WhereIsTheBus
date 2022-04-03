namespace WhereIsTheBus.TelegramBot.Telegram;

public interface IHandleUpdateService
{
    Task EchoAsync(Update update);
}