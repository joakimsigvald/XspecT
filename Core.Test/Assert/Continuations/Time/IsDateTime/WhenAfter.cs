using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsDateTime;

public class WhenAfter : Spec
{
    [Fact] public void GivenAfter_ThenDoesNotThrow() => A<DateTime>().Is().After(The<DateTime>().AddDays(-1));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = a.AddDays(1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().After(b));
        ex.Message.Is("A is after b");
        ex.InnerException.Message.Is($"Expected a to occur after {b} but found {a}");
    }
}