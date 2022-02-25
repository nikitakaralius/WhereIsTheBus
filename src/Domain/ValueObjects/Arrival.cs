namespace WhereIsTheBus.Domain.ValueObjects;

public record Arrival(TimeSpan ArrivesIn)
{
    public Arrival(int minutes) : this(new TimeSpan(0, minutes, 0)) { }

    public string Verbose() => $"{ArrivesIn.TotalMinutes} min";

    public override string ToString() => Verbose();
}
