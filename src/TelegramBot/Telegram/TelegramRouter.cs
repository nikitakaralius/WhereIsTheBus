using System.Reflection;
using WhereIsTheBus.TelegramBot.Attributes;
using WhereIsTheBus.TelegramBot.Queries;

namespace WhereIsTheBus.TelegramBot.Telegram;

internal class TelegramRouter : ITelegramRouter
{
    private static readonly Type BaseType = typeof(FromArgsQuery);
    
    public FromArgsQuery? QueryBy(string[] args)
    {
        var matchingQuery = MatchingQuery(args);

        if (matchingQuery is null)
        {
            return null;
        }
        
        var constructor = matchingQuery.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public,
            new[] {typeof(string[])});

        if (constructor is null)
        {
            throw new InvalidOperationException("Found a constructor that does not define a string[] argument");
        }

        return (FromArgsQuery) constructor.Invoke(new object?[] {args});
    }

    private static Type? MatchingQuery(string[] args) =>
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(TypeMatches)
            .FirstOrDefault(type => AttributeMatches(type, args));

    private static bool TypeMatches(Type type) =>
        type.IsAbstract == false
        && BaseType.IsAssignableFrom(type);

    private static bool AttributeMatches(Type type, string[] args)
    {
        var attribute = type.GetCustomAttribute<TelegramRoutesAttribute>();
        return attribute is not null && attribute.Names.Contains(args[0]);
    }
}