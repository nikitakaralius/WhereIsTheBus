namespace WhereIsTheBus.Domain.Entities;

public record RouteInfo(int Number, RouteDirection Direction, Transport Transport, IEnumerable<Station> Stations);
