namespace WhereIsTheBus.TelegramBot.Queries;

internal interface IQueryRouter
{
    FromArgsQuery QueryBy(string name);
}