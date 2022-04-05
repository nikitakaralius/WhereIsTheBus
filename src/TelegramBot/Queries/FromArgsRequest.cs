namespace WhereIsTheBus.TelegramBot.Queries;

internal abstract class FromArgsRequest<TParam> : IRequest
{
    protected FromArgsRequest(IReadOnlyList<string> args) => Args = args;

    protected IReadOnlyList<string> Args { get; }
    
    public abstract TParam? Value { get; }
}