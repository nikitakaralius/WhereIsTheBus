namespace WhereIsTheBus.TelegramBot.Queries;

public abstract class FromUpdateQuery : IRequest
{
    protected FromUpdateQuery(UpdateEvent update)
    {
        Update = update;
    }
    
    public UpdateEvent Update { get; }
}