namespace WhereIsTheBus.Domain.Exceptions;

public class UnsupportedRouteException : Exception
{
    public UnsupportedRouteException(int route)
        : base($"Route \"{route}\" is not supported") { }
}
