using System.Reflection;
using WhereIsTheBus.TelegramBot.Attributes;
using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Telegram;

internal sealed class TelegramRequestRouter : ITelegramRequestRouter
{
    private static readonly IEnumerable<Type> BaseTypes = new[]
    {
        typeof(FromMessageQuery)
    };
    
    public IRequest? RequestFrom(Message message)
    {
        if (message.Text is null)
        {
            return null;
        }
        
        var matchingQuery = MatchingQuery(message.Text.Split());

        if (matchingQuery is null)
        {
            return null;
        }
        
        var constructor = matchingQuery.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public,
            new[] {typeof(Message)});

        if (constructor is null)
        {
            throw new InvalidOperationException("Found a constructor that does not define a Message argument");
        }

        return (FromMessageQuery) constructor.Invoke(new object?[] {message});
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