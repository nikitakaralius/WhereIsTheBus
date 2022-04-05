namespace WhereIsTheBus.TelegramBot.Queries;

internal abstract class FromMessageQuery<TParam> : FromMessageQuery
{
    protected FromMessageQuery(Message message) : base(message) { }
    
    public abstract TParam? Value { get; }
}

internal abstract class FromMessageQuery : IRequest
{
    protected FromMessageQuery(Message message) { }
}