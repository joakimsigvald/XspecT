using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsDateTime;

public class WhenNotAfter : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNotAfter_ThenDoesNotThrow(int days)
        => A<DateTime>().Is().NotAfter(The<DateTime>().AddDays(days));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = a.AddDays(-1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().NotAfter(b));
        ex.Message.Is("A is not after b");
        ex.InnerException.Message.Is($"Expected a to occur not after {b} but found {a}");
    }
}