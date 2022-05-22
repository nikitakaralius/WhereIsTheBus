namespace WhereIsTheBus.TelegramBot.Domain.Exceptions;

internal sealed class InvalidDirectionConversion : Exception
{
    public InvalidDirectionConversion(Direction direction) 
        : base($"Can not convert {direction} to StrictDirection")
    {
        Direction = direction;
    }
    
    public Direction Direction { get; }
}