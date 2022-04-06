namespace WhereIsTheBus.Domain.Tests.Records;

[TestFixture]
public class TransportRouteTests
{
    [Test]
    [TestCase(new object?[] {new[] {"bus", "29"}})]
    [TestCase(new object?[] {new[] {"troll", "14"}})]
    [TestCase(new object?[] {new[] {"tram", "10"}})]
    public void ParsingWithoutDirectionShouldGiveRouteWithBothDirections(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().NotBeNull();
        route!.Direction.Should().Be(Direction.Both);
    }

    [Test]
    [TestCase(new object?[] {new[] {"bus", "d"}})]
    [TestCase(new object?[] {new[] {"troll", "r"}})]
    [TestCase(new object?[] {new[] {"tram", "d"}})]
    public void ParsingRouteWithoutNumberShouldReturnNull(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().BeNull();
    }

    [Test]
    [TestCase(new object?[] {new[] {"bus", "29.5", "d"}})]
    [TestCase(new object?[] {new[] {"bus", "bus"}})]
    [TestCase(new object?[] {new[] {"tram", "r"}})]
    [TestCase(new object?[] {new[] {"troll", "+"}})]
    public void ParsingRouteWithNoIntNumberArgumentShouldReturnNull(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().BeNull();
    }

    [Test]
    [TestCase(new object?[] {new[] {"bus", "-29", "d"}})]
    [TestCase(new object?[] {new[] {"bus", "-29"}})]
    [TestCase(new object?[] {new[] {"troll", "0"}})]
    [TestCase(new object?[] {new[] {"tram", "0", "r"}})]
    public void ParsingRouteWithZeroOrNegativeNumberShouldReturnNull(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().BeNull();
    }

    [Test]
    [TestCase(new object?[] {new string[] { }})]
    [TestCase(new object?[] {new[] {"bus"}})]
    [TestCase(new object?[] {new[] {"troll"}})]
    [TestCase(new object?[] {new[] {"tram"}})]
    public void LackOfArgumentsShouldReturnNull(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().BeNull();
    }

    [Test]
    [TestCase(new object?[] {new[] {"plane", "100", "d"}})]
    [TestCase(new object?[] {new[] {"boat", "5"}})]
    public void ParsingUnknownTransportTypeShouldReturnNull(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().BeNull();
    }

    [Test]
    [TestCase(new object?[] {new[] {"bus", "29", "d"}})]
    [TestCase(new object?[] {new[] {"b", "79"}})]
    [TestCase(new object?[] {new[] {"troll", "7"}})]
    [TestCase(new object?[] {new[] {"tr", "10", "r"}})]
    [TestCase(new object?[] {new[] {"tram", "10", "d"}})]
    [TestCase(new object?[] {new[] {"t", "7"}})]
    public void CanParseKnownTransportTypes(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().NotBeNull();
        route!.Number.Should().Be(int.Parse(args[1]));
    }

    [Test]
    [TestCase(new object?[] {new[] {"bus", "29", "d"}})]
    [TestCase(new object?[] {new[] {"tram", "1", "direct"}})]
    [TestCase(new object?[] {new[] {"troll", "7", "1"}})]
    public void CanParseDirectDirection(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().NotBeNull();
        route!.Direction.Should().Be(Direction.Direct);
    }

    [Test]
    [TestCase(new object?[] {new[] {"bus", "29", "r"}})]
    [TestCase(new object?[] {new[] {"tram", "1", "return"}})]
    [TestCase(new object?[] {new[] {"troll", "7", "2"}})]
    public void CanParseReturnDirection(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().NotBeNull();
        route!.Direction.Should().Be(Direction.Return);
    }

    [Test]
    [TestCase(new object?[] {new[] {"troll", "7", "0"}})]
    [TestCase(new object?[] {new[] {"bus", "29", "0"}})]
    [TestCase(new object?[] {new[] {"t", "7", "0"}})]
    public void ParsingNonDirectionShouldGiveBothDirections(string[] args)
    {
        TransportRoute? route = TransportRoute.Parse(args);
        route.Should().NotBeNull();
        route!.Direction.Should().Be(Direction.Both);
    }
}