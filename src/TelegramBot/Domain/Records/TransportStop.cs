namespace WhereIsTheBus.TelegramBot.Domain.Records;

internal sealed record TransportStop(int Id, string Name, StrictDirection Direction, int TimeToArrive)
{
    public bool Equals(TransportStop? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}