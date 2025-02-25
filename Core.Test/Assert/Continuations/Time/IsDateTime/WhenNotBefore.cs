using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsDateTime;

public class WhenNotBefore : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenNotBefore_ThenDoesNotThrow(int days)
        => A<DateTime>().Is().NotBefore(The<DateTime>().AddDays(days));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = a.AddDays(1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().NotBefore(b));
        ex.Message.Is("A is not before b");
        ex.InnerException.Message.Is($"Expected a to occur not before {b} but found {a}");
    }
}