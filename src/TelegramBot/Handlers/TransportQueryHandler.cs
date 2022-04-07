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
        InlineKeyboardMarkup keyboard = new(AllRoutes().Select(RouteButtons));
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

    private int[] AllRoutes() =>
        new[]
        {
            2, 6, 7, 8, 9, 11, 12, 15, 16, 19, 21, 22, 23, 25, 26, 27, 28, 29, 31, 34, 36, 40, 41, 45, 56, 68, 73, 79
        };
}