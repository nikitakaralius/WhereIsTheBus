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
        int[] routes = request.Update.UserMessage switch
        {
            "Автобусы"    => AllBusRoutes(),
            "Троллейбусы" => AllTrolleyBusRoutes(),
            "Трамваи"     => AllTramRoutes(),
            _ => throw new ArgumentOutOfRangeException(
                nameof(request), $"This handler is not supposed for {request.Update.UserMessage} request")
        };
        InlineKeyboardMarkup keyboard = new(routes.Select(RouteButtons));
        await _telegram.SendTextMessageAsync(request.Update.ChatId, "Выберите маршрут", replyMarkup: keyboard,
                                             cancellationToken: cancellationToken);
        return Unit.Value;
    }

    private InlineKeyboardButton[] RouteButtons(int route) =>
        new[]
        {
            InlineKeyboardButton.WithCallbackData($"{route}", $"/bus {route}"),
            InlineKeyboardButton.WithCallbackData("Прямое", $"/bus {route} d"),
            InlineKeyboardButton.WithCallbackData("Обратное", $"/bus {route} r")
        };

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