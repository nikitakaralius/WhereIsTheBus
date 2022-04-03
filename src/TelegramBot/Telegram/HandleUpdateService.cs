using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;

namespace WhereIsTheBus.TelegramBot.Telegram;

internal class HandleUpdateService : IHandleUpdateService
{
    private readonly ITelegramBotClient _client;
    private readonly ILogger<HandleUpdateService> _logger;

    public HandleUpdateService(ITelegramBotClient client, ILogger<HandleUpdateService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task EchoAsync(Update update)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => OnMessageReceived(update.Message!),
            _ => OnUnknownUpdateReceived(update)
        };
        try
        {
            await handler;
        }
        catch (Exception e)
        {
            await HandleErrorAsync(e);
        }
    }

    private async Task OnMessageReceived(Message message)
    {
        await _client.SendTextMessageAsync(message.Chat.Id, "Hello, World!");
    }

    private async Task OnUnknownUpdateReceived(Update update)
    {
        
    }
    
    private Task HandleErrorAsync(Exception exception)
    {
        string errorMessage = exception switch
        {
            ApiRequestException apiRequestException => 
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.Message
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", errorMessage);
        return Task.CompletedTask;
    }
}