namespace WhereIsTheBus.Domain.ValueObjects;

public record Arrival(TimeSpan ArrivesIn)
{
    public string Verbose()
    {
        throw new NotImplementedException();
    }
}
