namespace WhereIsTheBus.TelegramBot.Queries;

internal abstract class FromUpdateQuery<TParam> : FromUpdateQuery
{
    protected FromUpdateQuery(UpdateEvent update) : base(update) { }
    
    public abstract TParam? Value { get; }
}

internal abstract class FromUpdateQuery : IRequest
{
    protected FromUpdateQuery(UpdateEvent update)
    {
        Update = update;
    }
    
    public UpdateEvent Update { get; }
}