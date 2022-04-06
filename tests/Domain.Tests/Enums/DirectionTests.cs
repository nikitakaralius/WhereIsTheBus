namespace WhereIsTheBus.Domain.Tests.Enums;

[TestFixture]
public class DirectionTests
{
    [Test]
    public void ConvertingBothDirectionsToStrictDirectionShouldThrowException()
    {
        var direction = Direction.Both;
        Action convert = () => direction.AsStrictDirection();
        convert.Should().Throw<InvalidDirectionConversion>();
    }
}