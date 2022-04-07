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
            InlineKeyboardButton.WithCallbackData("29", "/bus 29"), 
            InlineKeyboardButton.WithCallbackData("79", "/bus 79"), 
            InlineKeyboardButton.WithCallbackData("28", "/bus 28"), 
        });
        await _telegram.SendTextMessageAsync(request.Update.ChatId, "Выберите маршрут", replyMarkup: keyboard,
            cancellationToken: cancellationToken);
        return Unit.Value;
    }
    
}