using WhereIsTheBus.TelegramBot.Domain.Exceptions;

namespace WhereIsTheBus.TelegramBot.Domain.Enums;

internal enum StrictDirection
{
    None = 0,
    Direct = 1,
    Return = 2
}

internal static class DirectionExtensions
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