namespace WhereIsTheBus.TelegramBot.Queries;

internal abstract class FromMessageQuery<TParam> : FromMessageQuery
{
    protected FromMessageQuery(Message message) : base(message) { }
    
    public abstract TParam? Value { get; }
}

public abstract class FromMessageQuery : IRequest
{
    protected FromMessageQuery(Message message)
    {
        Message = message;
    }
    
    public Message Message { get; }
}