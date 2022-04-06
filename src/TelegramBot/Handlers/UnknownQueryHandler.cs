using Telegram.Bot.Types.Enums;

namespace WhereIsTheBus.TelegramBot.Handlers;

internal class UnknownQueryHandler : IRequestHandler<UnknownQuery>
{
    private readonly ITelegramBotClient _client;

    public UnknownQueryHandler(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task<Unit> Handle(UnknownQuery request, CancellationToken cancellationToken)
    {
        const string message = "*Извините, мы не знаем такую команду*";
        await _client.SendTextMessageAsync(request.Update.ChatId,
                                           message, ParseMode.Markdown,
                                           cancellationToken: cancellationToken);
        return Unit.Value;
    }
}