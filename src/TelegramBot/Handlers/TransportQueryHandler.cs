using Telegram.Bot.Types.ReplyMarkups;

namespace WhereIsTheBus.TelegramBot.Handlers;

public class TransportQueryHandler : IRequestHandler<TransportQuery>
{
    private readonly ITelegramBotClient _telegram;

    public TransportQueryHandler(ITelegramBotClient telegram)
    {
        _telegram = telegram;
    }

    public async Task<Unit> Handle(TransportQuery request, CancellationToken cancellationToken)
    {
        var routes = request.Update.UserMessage switch
        {
            "Автобусы"    => AllBusRoutes().Select(x => RouteButtons(x, TransportType.Bus)),
            "Троллейбусы" => AllBusRoutes().Select(x => RouteButtons(x, TransportType.Trolleybus)),
            "Трамваи"     => AllBusRoutes().Select(x => RouteButtons(x, TransportType.Tram)),
            _ => throw new ArgumentOutOfRangeException(
                nameof(request), $"This handler is not supposed for {request.Update.UserMessage} request")
        };
        InlineKeyboardMarkup keyboard = new(routes);
        await _telegram.SendTextMessageAsync(request.Update.ChatId, "Выберите маршрут", replyMarkup: keyboard,
                                             cancellationToken: cancellationToken);
        return Unit.Value;
    }

    private InlineKeyboardButton[] RouteButtons(int route, TransportType transport)
    {
        string command = CommandOf(transport);
        return new[]
        {
            InlineKeyboardButton.WithCallbackData($"{route}", $"/{command} {route}"),
            InlineKeyboardButton.WithCallbackData("Прямое", $"/{command} {route} d"),
            InlineKeyboardButton.WithCallbackData("Обратное", $"/{command} {route} r")
        };
    }

    private string CommandOf(TransportType transport)
    {
        return transport switch
        {
            TransportType.Bus        => "/bus",
            TransportType.Trolleybus => "/troll",
            TransportType.Tram       => "/tram",
            TransportType.None or _  => throw new ArgumentOutOfRangeException(nameof(transport), transport, null)
        };
    }

    private int[] AllBusRoutes()
    {
        return new[]
        {
            2, 6, 7, 8, 9, 11, 12, 15, 16, 19, 21, 22, 23, 25, 26,
            27, 28, 29, 31, 34, 36, 40, 41, 45, 56, 68, 73, 79
        };
    }

    private int[] AllTrolleyBusRoutes()
    {
        return new[]
        {
            1, 2, 4, 6, 7, 9, 10, 14
        };
    }

    private int[] AllTramRoutes()
    {
        return new[]
        {
            1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 12
        };
    }
}