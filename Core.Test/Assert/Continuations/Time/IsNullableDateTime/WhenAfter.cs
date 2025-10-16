using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsNullableDateTime;

public class WhenAfter : Spec
{
    [Fact]
    public void GivenAfter_ThenDoesNotThrow()
        => A<DateTime?>().Is().After(The<DateTime?>()!.Value.AddDays(-1))
        .And.CloseTo(The<DateTime?>()!.Value, TimeSpan.Zero);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime?>();
        var b = a!.Value.AddDays(1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().After(b));
        ex.Message.Is("A is after b");
        ex.HasInnerMessage($"Expected a to occur after {b} but found {a}");
    }
}