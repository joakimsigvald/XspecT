using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Time.IsTimeSpan;

public class WhenPositive : Spec
{
    [Fact]
    public void GivenPositive_ThenDoesNotThrow()
        => TimeSpan.FromDays(1).Is().Positive();

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenFail_ThenGetException(int days)
    {
        var a = TimeSpan.FromDays(days);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().Positive());
        ex.Message.Is("A is positive");
        ex.InnerException.Message.Is($"Expected a to be positive but found {a}");
    }
}