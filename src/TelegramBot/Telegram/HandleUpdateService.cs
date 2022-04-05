namespace WhereIsTheBus.TelegramBot.Telegram;

internal class HandleUpdateService : IHandleUpdateService
{
    private readonly IMediator _mediator;
    private readonly ITelegramRouter _router;

    public HandleUpdateService(IMediator mediator, ITelegramRouter router)
    {
        _mediator = mediator;
        _router = router;
    }

    public async Task EchoAsync(Update update)
    {
        if (update.Message is null)
        {
            return;
        }

        var query = _router.QueryFrom(update.Message!);

        if (query is null)
        {
            return;
        }
        
        await _mediator.Send(query);
    }
}