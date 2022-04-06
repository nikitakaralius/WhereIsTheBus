using System.Text;
using Telegram.Bot.Types.Enums;
using WhereIsTheBus.Domain.Enums;
using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Handlers;

internal class TransportRouteHandler : IRequestHandler<TransportRouteQuery>
{
    private const int AverageMessageLength = 200;
    private readonly IScheduleClient _scheduleClient;
    private readonly ITelegramBotClient _telegramClient;
    private readonly ILogger<TransportRouteHandler> _logger;

    public TransportRouteHandler(IScheduleClient scheduleClient,
                                 ITelegramBotClient telegramClient,
                                 ILogger<TransportRouteHandler> logger)
    {
        _scheduleClient = scheduleClient;
        _telegramClient = telegramClient;
        _logger = logger;
    }

    public async Task<Unit> Handle(TransportRouteQuery request, CancellationToken cancellationToken)
    {
        if (request.Value is null)
        {
            _logger.LogInformation("Provided incomplete route data");
            return Unit.Value;
        }

        IEnumerable<TransportStop> stops = await _scheduleClient.StopsAsync(request.Value);
        string message = GenerateMessage(from: stops);
        await _telegramClient.SendTextMessageAsync(
            request.Message.Chat.Id, message,
            ParseMode.Markdown,
            cancellationToken: cancellationToken);
        return Unit.Value;
    }

    private string GenerateMessage(IEnumerable<TransportStop> from)
    {
        StringBuilder sb = new(AverageMessageLength);

        foreach ((int _, string name, var direction, int timeToArrive) in from)
        {
            string directionCharacter = direction switch
            {
                StrictDirection.Direct => "⬇️",
                StrictDirection.Return => "⬆️",
                StrictDirection.None or _ => throw new ArgumentOutOfRangeException(
                    nameof(direction), "Direction can only be direct and return")
            };

            sb.Append($"{directionCharacter} *{name}*: _{timeToArrive} мин._").AppendLine();
        }

        return sb.ToString();
    }
}