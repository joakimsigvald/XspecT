using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsTimeSpan;

public class WhenNotNegative : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNotNegative_ThenDoesNotThrow(int days)
        => TimeSpan.FromDays(days).Is().Not().Negative();

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = TimeSpan.FromDays(-1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().Negative());
        ex.Message.Is("A is not negative");
        ex.InnerException.Message.Is($"Expected a to not be negative but found {a}");
    }
}