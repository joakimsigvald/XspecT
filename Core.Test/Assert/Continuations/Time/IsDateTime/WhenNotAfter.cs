using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsDateTime;

public class WhenNotAfter : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNotAfter_ThenDoesNotThrow(int days)
        => A<DateTime>().Is().Not().After(The<DateTime>().AddDays(days));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = a.AddDays(-1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().After(b));
        ex.Message.Is("A is not after b");
        ex.InnerException.Message.Is($"Expected a to not occur after {b} but found {a}");
    }
}