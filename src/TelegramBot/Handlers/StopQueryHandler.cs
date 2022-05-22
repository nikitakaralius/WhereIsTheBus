using System.Text;
using Telegram.Bot.Types.Enums;
using Route = WhereIsTheBus.Domain.Records.Route;

namespace WhereIsTheBus.TelegramBot.Handlers;

internal sealed class StopQueryHandler : IRequestHandler<StopQuery>
{
    private const int AverageMessageLength = 500;

    private readonly ITelegramBotClient _telegram;
    private readonly IScheduleClient _schedule;

    public StopQueryHandler(ITelegramBotClient telegram, IScheduleClient schedule)
    {
        _telegram = telegram;
        _schedule = schedule;
    }

    public async Task<Unit> Handle(StopQuery request, CancellationToken cancellationToken)
    {
        if (request.StopId is null)
        {
            return Unit.Value;
        }

        IEnumerable<Transport> transport = await _schedule.TransportAsync(request.StopId.Value);
        transport = transport as Transport[] ?? transport.ToArray();
        
        string message = transport.Any()
            ? GenerateMessage(from: transport)
            : "Извините, мы не можем найти информацию об этой остановке";

        await _telegram.SendTextMessageAsync(request.Update.ChatId,
                                             message,
                                             ParseMode.Markdown,
                                             cancellationToken: cancellationToken);
        
        return Unit.Value;
    }

    private string GenerateMessage(IEnumerable<Transport> from)
    {
        StringBuilder sb = new(AverageMessageLength);
        foreach ((var transport, IEnumerable<Arrival> routes) in from)
        {
            sb.Append($"*{transport}*\n");
            foreach (var route in routes)
            {
                string time = route.HasValidTime ? $"_{route.TimeToArrive} мин._" : $"_{route.TimeToArrive}_";
                sb.Append($"{route.Number}: {time}\n");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}