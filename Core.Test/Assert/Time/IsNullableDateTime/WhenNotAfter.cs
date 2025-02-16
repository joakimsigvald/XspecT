using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsNullableDateTime;

public class WhenNotAfter : Spec
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GivenNotAfter_ThenDoesNotThrow(int days)
        => A<DateTime?>().Is().NotAfter(The<DateTime?>().Value.AddDays(days))
        .And.CloseTo(The<DateTime?>().Value, TimeSpan.Zero);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime?>();
        var b = a.Value.AddDays(-1);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotAfter(b));
        ex.Message.Is("A is not after b");
        ex.InnerException.Message.Is($"Expected a to occur not after {b} but found {a}");
    }
}