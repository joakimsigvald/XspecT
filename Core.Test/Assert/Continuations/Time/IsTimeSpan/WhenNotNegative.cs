using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Time.IsTimeSpan;

public class WhenNotNegative : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNotNegative_ThenDoesNotThrow(int days)
        => TimeSpan.FromDays(days).Is().NotNegative();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = TimeSpan.FromDays(-1);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotNegative());
        ex.Message.Is("A is not negative");
        ex.InnerException.Message.Is($"Expected a to be not negative but found {a}");
    }
}