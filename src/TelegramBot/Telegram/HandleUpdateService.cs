using System.Text;
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
        string[] args = message.Text!.Split();
        string response = args[0] switch
        {
            "/29" => await BusResponse(args),
            _     => $"Sorry, we don't handle {message.Text}"
        };
        await _telegramClient.SendTextMessageAsync(message.Chat.Id, response, ParseMode.Markdown);
    }

    private Task OnUnknownUpdateReceived(Update update)
    {
        return Task.CompletedTask;
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

    private async Task<string> BusResponse(string[] args)
    {
        Direction direction = args.Length < 1
            ? Direction.Direct
            : args[1] switch
            {
                "d" or "0" => Direction.Direct,
                "r" or "1" => Direction.Return,
                _ => Direction.Direct
            };
        TransportRoute route = new(Transport.Bus, 29, direction);
        IEnumerable<Stop> stops = await _scheduleClient.StopsAsync(route);
        stops = stops.Distinct();
        StringBuilder sb = new (200);
        foreach ((int _, string name, int timeToArrive) in stops)
        {
            sb.Append('*').Append(name).Append("* : ")
              .Append(timeToArrive).Append(" мин.").AppendLine();
        }
        return sb.ToString();
    }
}