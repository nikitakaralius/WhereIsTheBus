namespace WhereIsTheBus.TelegramBot.Domain.Records;

internal sealed record Arrival(int TransportNumber, string TimeToArrive)
{
    public bool HasValidTime => int.TryParse(TimeToArrive, out _);
}