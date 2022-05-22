namespace WhereIsTheBus.TelegramBot.Queries;

internal abstract class FromUpdateQuery : IRequest
{
    protected FromUpdateQuery(UpdateEvent update)
    {
        Update = update;
    }
    
    public UpdateEvent Update { get; }
}