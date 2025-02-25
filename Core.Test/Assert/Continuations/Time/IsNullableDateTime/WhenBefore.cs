using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsNullableDateTime;

public class WhenBefore : Spec
{
    [Fact]
    public void GivenBefore_ThenDoesNotThrow()
        => A<DateTime?>().Is().Before(The<DateTime?>().Value.AddDays(1))
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime?>();
        var b = a.Value.AddDays(-1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Before(b));
        ex.Message.Is("A is before b");
        ex.InnerException.Message.Is($"Expected a to occur before {b} but found {a}");
    }
}