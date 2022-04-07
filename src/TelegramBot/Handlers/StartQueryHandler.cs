using Telegram.Bot.Types.ReplyMarkups;

namespace WhereIsTheBus.TelegramBot.Handlers;

public class StartQueryHandler : IRequestHandler<StartQuery>
{
    private readonly ITelegramBotClient _telegram;

    public StartQueryHandler(ITelegramBotClient telegram)
    {
        _telegram = telegram;
    }

    public async Task<Unit> Handle(StartQuery request, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup keyboard = new(new KeyboardButton[]
        {
            new("Автобусы"),
            new("Троллейбусы"),
            new("Трамваи")
        })
        {
            ResizeKeyboard = true
        };
        await _telegram.SendTextMessageAsync(request.Update.ChatId, "Выберите транспорт", replyMarkup: keyboard,
            cancellationToken: cancellationToken);
        return Unit.Value;
    }
}