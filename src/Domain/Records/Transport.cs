namespace WhereIsTheBus.Domain.Records;

public record Transport(string Name, IEnumerable<Route> Routes);