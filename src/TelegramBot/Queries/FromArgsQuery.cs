namespace WhereIsTheBus.TelegramBot.Queries;

internal abstract class FromArgsQuery<TParam> : FromArgsQuery
{
    protected FromArgsQuery(string[] args) : base(args)
    {
    }
    
    public abstract TParam? Value { get; }
}

internal abstract class FromArgsQuery : IRequest
{
    protected FromArgsQuery(string[] args) { }
}