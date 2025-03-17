using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsNullableDateTime;

public class WhenNotBefore : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenNotBefore_ThenDoesNotThrow(int days)
        => A<DateTime?>().Is().Not().Before(The<DateTime?>().Value.AddDays(days))
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime?>();
        var b = a.Value.AddDays(1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().Before(b));
        ex.Message.Is("A is not before b");
        ex.InnerException.Message.Is($"Expected a to not occur before {b} but found {a}");
    }
}