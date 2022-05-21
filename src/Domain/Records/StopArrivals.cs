namespace WhereIsTheBus.Domain.Records;

public record StopArrivals(TransportType Transport, IEnumerable<Arrival> Arrivals);