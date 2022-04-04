namespace WhereIsTheBus.TelegramBot.Models.Internal;

internal class Stop : IEquatable<Stop>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int TimeToArrive { get; init; }

    public void Deconstruct(out int Id, out string Name, out int TimeToArrive)
    {
        Id = this.Id;
        Name = this.Name;
        TimeToArrive = this.TimeToArrive;
    }

    public bool Equals(Stop? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Stop) obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}