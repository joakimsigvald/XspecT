using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsTimeSpan;

public class WhenNegative : Spec
{
    [Fact]
    public void GivenNegative_ThenDoesNotThrow()
        => TimeSpan.FromDays(-1).Is().Negative();

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenFail_ThenGetException(int days)
    {
        var a = TimeSpan.FromDays(days);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().Negative());
        ex.Message.Is("A is negative");
        ex.InnerException.Message.Is($"Expected a to be negative but found {a}");
    }
}