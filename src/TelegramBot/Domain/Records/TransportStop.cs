namespace WhereIsTheBus.TelegramBot.Domain.Records;

public record TransportStop(int Id, string Name, StrictDirection Direction, int TimeToArrive)
{
    public virtual bool Equals(TransportStop? other)
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