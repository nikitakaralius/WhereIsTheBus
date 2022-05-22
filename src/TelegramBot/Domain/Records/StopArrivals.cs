namespace WhereIsTheBus.TelegramBot.Domain.Records;

internal sealed record StopArrivals(TransportType Transport, IEnumerable<Arrival> Arrivals);