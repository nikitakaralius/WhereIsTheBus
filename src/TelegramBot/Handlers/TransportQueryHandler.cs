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
        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("29", "/bus 29"),
                InlineKeyboardButton.WithCallbackData("Прямое", "/bus 29 d"),
                InlineKeyboardButton.WithCallbackData("Обратное", "/bus 29 r")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("28", "/bus 28"),
                InlineKeyboardButton.WithCallbackData("Прямое", "/bus 28 d"),
                InlineKeyboardButton.WithCallbackData("Обратное", "/bus 28 r")
            }
        });
        await _telegram.SendTextMessageAsync(request.Update.ChatId, "Выберите маршрут", replyMarkup: keyboard,
            cancellationToken: cancellationToken);
        return Unit.Value;
    }
}