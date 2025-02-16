using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsDateTime;

public class WhenCloseTo : Spec
{
    [Theory]
    [InlineData(0, 0)]
    public void GivenCloseTo_ThenDoesNotThrow(int days, int presisionDays) 
        => A<DateTime>().Is().CloseTo(The<DateTime>().AddDays(days), TimeSpan.FromDays(presisionDays));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = The<DateTime>().AddDays(1);
        var precision = TimeSpan.FromDays(0);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().CloseTo(b, precision));
        ex.Message.Is("A is close to b");
        ex.InnerException.Message.Is($"Expected a to be close to {b} but found {a}");
    }
}