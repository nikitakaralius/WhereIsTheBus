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
        if (request.Transport is null)
        {
            return Unit.Value;
        }

        InlineKeyboardMarkup keyboard = new(RouteButtons(request));
        await _telegram.SendTextMessageAsync(request.Update.ChatId, "Выберите маршрут", replyMarkup: keyboard,
                                             cancellationToken: cancellationToken);
        return Unit.Value;
    }

    private IEnumerable<InlineKeyboardButton[]> RouteButtons(TransportQuery query)
    {
        var transport = query.Transport!.Value;
        IEnumerable<int> routes = transport switch
        {
            TransportType.Bus when !query.FullList => CommonBusRoutes(),
            TransportType.Bus when query.FullList  => ExtraBusRoutes(),
            TransportType.Trolleybus               => AllTrolleybusesRoutes(),
            TransportType.Tram                     => AllTramRoutes(),
            TransportType.None or _                => throw new ArgumentOutOfRangeException(nameof(query))
        };

        var buttons = routes.Select(x => ButtonsFor(x, transport));

        if (transport == TransportType.Bus && query.FullList == false)
        {
            buttons = buttons.Append(new[]
            {
                InlineKeyboardButton.WithCallbackData("Другие маршруты", "Автобусы все"),
            });
        }
        
        return buttons;
    }

    private InlineKeyboardButton[] ButtonsFor(int route, TransportType transport)
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
            TransportType.Bus        => "bus",
            TransportType.Trolleybus => "troll",
            TransportType.Tram       => "tram",
            TransportType.None or _  => throw new ArgumentOutOfRangeException(nameof(transport), transport, null)
        };
    }

    private IEnumerable<int> CommonBusRoutes() => new[] {8, 12, 18, 21, 22, 26, 27, 28, 29, 36, 79};

    private IEnumerable<int> ExtraBusRoutes() => new[] {2, 6, 7, 11, 15, 16, 23, 25, 31, 34, 40, 41, 45, 56, 68, 73};

    private IEnumerable<int> AllTrolleybusesRoutes() => new[] {1, 2, 4, 6, 7, 9, 10, 14};

    private IEnumerable<int> AllTramRoutes() => new[] {1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 12};
}