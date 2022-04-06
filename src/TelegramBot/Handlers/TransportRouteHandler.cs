using System.Text;
using Telegram.Bot.Types.Enums;
using WhereIsTheBus.Domain.Enums;
using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Handlers;

internal class TransportRouteHandler : IRequestHandler<TransportRouteQuery>
{
    private const int AverageMessageLength = 1500;

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
        string message = GenerateMessageFrom(request.Value, stops);
        await SendTextMessageAsync(request, message, cancellationToken);
        return Unit.Value;
    }

    private string GenerateMessageFrom(TransportRoute route, IEnumerable<TransportStop> stops)
    {
        if (stops.Any() == false)
        {
            return $"*Мы не можем найти информацию о вашем маршруте: {VerboseTransport(route)}.\n" +
                   $"Проверьте ваши данные или попробуйте позже*";
        }
        
        StringBuilder sb = new(AverageMessageLength);

        sb.Append(GenerateRouteMessage(from: route));

        stops = stops.Distinct();
        StrictDirection previousDirection = stops.First().Direction;
        
        foreach ((int _, string name, var direction, int timeToArrive) in stops)
        {
            string directionCharacter = direction switch
            {
                StrictDirection.Direct => "⬇️",
                StrictDirection.Return => "⬆️",
                StrictDirection.None or _ => throw new ArgumentOutOfRangeException(
                    nameof(direction), "Direction can only be direct and return")
            };

            if (previousDirection != direction)
            {
                sb.Append("\n\n");
            }
            
            sb.Append($"{directionCharacter} *{name}*: _{timeToArrive} мин._").AppendLine();
            previousDirection = direction;
        }

        return sb.ToString();
    }

    private string GenerateRouteMessage(TransportRoute from)
    {
        string verboseTransport = VerboseTransport(from);
        string verboseDirection = VerboseDirection(from.Direction);
        return $"*Выбран {verboseTransport}. {verboseDirection}.* \n\n";
    }

    private static string VerboseTransport(TransportRoute route) =>
        route.Transport switch
        {
            TransportType.Bus        => "🚌 автобус",
            TransportType.Trolleybus => "🚎 троллейбус",
            TransportType.Tram       => "🚃 трамвай",
            TransportType.None or _ => throw new ArgumentOutOfRangeException(
                nameof(route), "Transport type should be defined")
        } + $" №{route.Number}";

    private static string VerboseDirection(Direction direction) =>
        direction switch
        {
            Direction.Direct => "Прямое напрвление",
            Direction.Return => "Обратное напрвление",
            Direction.Both   => "Все направления",
            Direction.None or _ => throw new ArgumentOutOfRangeException(
                nameof(direction), "Direction type should be defined")
        };

    private Task<Message> SendTextMessageAsync(FromMessageQuery query,
                                               string message,
                                               CancellationToken cancellationToken) =>
        _telegramClient.SendTextMessageAsync(
            query.Message.Chat.Id, message,
            ParseMode.Markdown,
            cancellationToken: cancellationToken);
}