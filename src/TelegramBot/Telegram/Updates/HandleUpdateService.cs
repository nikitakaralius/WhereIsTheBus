using Telegram.Bot.Types.Enums;
using WhereIsTheBus.TelegramBot.Telegram.RequestRouter;

namespace WhereIsTheBus.TelegramBot.Telegram.Updates;

internal sealed class HandleUpdateService : IHandleUpdateService
{
    private readonly IMediator _mediator;
    private readonly ITelegramRequestRouter _router;
    private readonly ILogger<HandleUpdateService> _logger;

    public HandleUpdateService(IMediator mediator, 
                               ITelegramRequestRouter router,
                               ILogger<HandleUpdateService> logger)
    {
        _mediator = mediator;
        _router = router;
        _logger = logger;
    }

    public async Task EchoAsync(Update update)
    {
        var updateEvent = update.Type switch
        {
            UpdateType.Message => UpdateEvent.FromMessage(update.Message!),
            UpdateType.CallbackQuery => UpdateEvent.FromCallbackQuery(update.CallbackQuery!),
            _ => null
        };

        if (updateEvent is null)
        {
            return;
        }

        var query = _router.RequestFrom(updateEvent);

        try
        {
            await _mediator.Send(query);
        }
        catch (Exception e)
        {
            _logger.LogError("Handle error {errorMessage}", e.Message);
        }
    }
}