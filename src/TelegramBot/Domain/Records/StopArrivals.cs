namespace WhereIsTheBus.TelegramBot.Domain.Records;

public record StopArrivals(TransportType Transport, IEnumerable<Arrival> Arrivals);