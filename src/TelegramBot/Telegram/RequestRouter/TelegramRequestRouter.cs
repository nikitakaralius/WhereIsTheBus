using System.Reflection;

namespace WhereIsTheBus.TelegramBot.Telegram.RequestRouter;

internal sealed class TelegramRequestRouter : ITelegramRequestRouter
{
    private static readonly IEnumerable<Type> BaseTypes = new[]
    {
        typeof(FromUpdateQuery)
    };

    private readonly Dictionary<string, Type> _queries;

    public TelegramRequestRouter(Dictionary<string, Type> queries)
    {
        _queries = queries;
    }

    public static TelegramRequestRouter InitializeFromAssemblyTypes(Assembly assembly)
    {
        var routes = assembly.GetTypes()
                                 .Where(TypeMatches)
                                 .Select(type => (Type: type, Attribute: type.GetCustomAttribute<TelegramRoutesAttribute>()))
                                 .Where(x => x.Attribute is not null)
                                 .ToList();

        Dictionary<string, Type> queries = new(routes.Count);
        foreach (var (type, attribute) in routes)
        {
            foreach (string route in attribute!.Names)
            {
                if (queries.TryAdd(route, type) == false)
                    throw new InvalidOperationException($"{type.FullName} has route that already exists");
            }
        }
        return new TelegramRequestRouter(queries);
    }

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

        return (IRequest) constructor.Invoke(new object?[] {update});
    }

    private Type? MatchingQuery(string[] args) => 
        _queries.TryGetValue(args[0], out var query) ? query : null;

    private static bool TypeMatches(Type type) =>
        type.IsAbstract == false
        && BaseTypes.Any(x => x.IsAssignableFrom(type));
}