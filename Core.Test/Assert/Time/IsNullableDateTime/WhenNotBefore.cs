using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsNullableDateTime;

public class WhenNotBefore : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenNotBefore_ThenDoesNotThrow(int days)
        => A<DateTime?>().Is().NotBefore(The<DateTime?>().Value.AddDays(days))
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime?>();
        var b = The<DateTime?>().Value.AddDays(1);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotBefore(b));
        ex.Message.Is("A is not before b");
        ex.InnerException.Message.Is($"Expected a to occur not before {b} but found {a}");
    }
}