using System.Reflection;

namespace WhereIsTheBus.TelegramBot.Telegram.RequestRouter;

internal sealed class TelegramRequestRouter : ITelegramRequestRouter
{
    private static readonly IEnumerable<Type> BaseTypes = new[]
    {
        typeof(FromUpdateQuery)
    };
    
    public IRequest RequestFrom(UpdateEvent update)
    {
        var matchingQuery = MatchingQuery(update.UserMessage.Split());

        if (matchingQuery is null)
        {
            return new UnknownQuery(update);
        }
        
        var constructor = matchingQuery.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public,
            new[] {typeof(UpdateEvent)});

        if (constructor is null)
        {
            throw new InvalidOperationException("Found a constructor that does not define a Message argument");
        }

        return (FromUpdateQuery) constructor.Invoke(new object?[] {update});
    }

    private static Type? MatchingQuery(string[] args) =>
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(TypeMatches)
            .FirstOrDefault(type => AttributeMatches(type, args));

    private static bool TypeMatches(Type type) =>
        type.IsAbstract == false
        && BaseTypes.Any(x => x.IsAssignableFrom(type));

    private static bool AttributeMatches(Type type, string[] args)
    {
        var attribute = type.GetCustomAttribute<TelegramRoutesAttribute>();
        return attribute is not null && attribute.Names.Contains(args[0]);
    }
}