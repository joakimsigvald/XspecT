using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Time.IsDateTime;

public class WhenBefore : Spec
{
    [Fact] public void GivenBefore_ThenDoesNotThrow() => A<DateTime>().Is().Before(The<DateTime>().AddDays(1));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = a.AddDays(-1);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().Before(b));
        ex.Message.Is("A is before b");
        ex.InnerException.Message.Is($"Expected a to occur before {b} but found {a}");
    }
}