namespace WhereIsTheBus.TelegramBot.Domain.Records;

public record Arrival(int TransportNumber, string TimeToArrive)
{
    public bool HasValidTime => int.TryParse(TimeToArrive, out _);
}