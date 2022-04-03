using System.Text.Json;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using WhereIsTheBus.TelegramBot.Services;

namespace WhereIsTheBus.TelegramBot.Telegram;

internal class HandleUpdateService : IHandleUpdateService
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly IScheduleClient _scheduleClient;
    private readonly ILogger<HandleUpdateService> _logger;

    public HandleUpdateService(ITelegramBotClient telegramClient,
                               ILogger<HandleUpdateService> logger,
                               IScheduleClient scheduleClient)
    {
        _telegramClient = telegramClient;
        _scheduleClient = scheduleClient;
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
        var response = message.Text!.Split()[0] switch
        {
            "/29" => await BusResponse(),
            _     => $"Sorry, can not handle {message.Text}"
        };
        await _telegramClient.SendTextMessageAsync(message.Chat.Id, response);
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

    private async Task<string> BusResponse()
    {
        TransportRoute route = new(Transport.Bus, 29, Direction.Return);
        IEnumerable<Stop> stops = await _scheduleClient.StopsAsync(route);
        return JsonSerializer.Serialize(stops);
    }
}