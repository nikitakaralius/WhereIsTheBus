using WhereIsTheBus.Domain.Exceptions;

namespace WhereIsTheBus.Domain.Enums;

public enum Direction
{
    None = 0,
    Direct = 1,
    Return = 2,
    Both = 3
}

public static class DirectionExtensions
{
    public static StrictDirection AsStrictDirection(this Direction direction) =>
        direction switch
        {
            Direction.None      => StrictDirection.None,
            Direction.Direct    => StrictDirection.Direct,
            Direction.Return    => StrictDirection.Return,
            Direction.Both or _ => throw new InvalidDirectionConversion(direction)
        };
}