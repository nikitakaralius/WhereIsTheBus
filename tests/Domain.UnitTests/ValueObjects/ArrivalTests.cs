namespace WhereIsTheBus.Domain.UnitTests.ValueObjects;

public class ArrivalTests
{
    [Fact]
    public void VerboseShouldReturnMinutes()
    {
        int minutes = 5;
        Arrival arrival = new(minutes);
        arrival.Verbose().Should().Contain($"{minutes}");
    }

    [Fact]
    public void ToStringShouldBeAsVerbose()
    {
        Arrival arrival = new(minutes: 20);
        arrival.ToString().Should().Be(arrival.Verbose());
    }
}
