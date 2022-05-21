namespace WhereIsTheBus.Domain.Records;

public record Route(int Number, string TimeToArrive)
{
    public bool HasValidTime => int.TryParse(TimeToArrive, out _);
}