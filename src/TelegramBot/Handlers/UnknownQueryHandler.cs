using Telegram.Bot.Types.Enums;
using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Handlers;

public class UnknownQueryHandler : IRequestHandler<UnknownQuery>
{
    private readonly ITelegramBotClient _client;

    public UnknownQueryHandler(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task<Unit> Handle(UnknownQuery request, CancellationToken cancellationToken)
    {
        const string message = "*Извините, мы не знаем такую команду*";
        await _client.SendTextMessageAsync(request.Message.Chat.Id,
                                           message, ParseMode.Markdown,
                                           cancellationToken: cancellationToken);
        return Unit.Value;
    }
}