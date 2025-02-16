using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsTimeSpan;

public class WhenNotPositive : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenNotPositive_ThenDoesNotThrow(int days)
        => TimeSpan.FromDays(days).Is().NotPositive();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = TimeSpan.FromDays(1);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotPositive());
        ex.Message.Is("A is not positive");
        ex.InnerException.Message.Is($"Expected a to be not positive but found {a}");
    }
}