using static WhereIsTheBus.TelegramBot.Domain.Enums.StrictDirection;

namespace WhereIsTheBus.TelegramBot.Tests.Records;

[TestFixture]
public class TransportStopTests
{
    [Test]
    public void StopsWithTheSameIdShouldBeEqual()
    {
        TransportStop a = new(1, "abc", Direct, 10);
        TransportStop b = new(1, "cba", Return, 15);
        a.Equals(b).Should().BeTrue();
        (a == b).Should().BeTrue();
    }
}