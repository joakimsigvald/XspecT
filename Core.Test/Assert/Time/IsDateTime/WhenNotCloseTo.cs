using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Time.IsDateTime;

public class WhenNotCloseTo : Spec
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(-2, 1)]
    public void GivenNotCloseTo_ThenDoesNotThrow(int days, int toleranceDays) 
        => A<DateTime>().Is().NotCloseTo(The<DateTime>().AddDays(days), TimeSpan.FromDays(toleranceDays));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<DateTime>();
        var b = The<DateTime>().AddDays(1);
        var tolerance = TimeSpan.FromDays(1);
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotCloseTo(b, tolerance));
        ex.Message.Is("A is not close to b");
        ex.InnerException.Message.Is($"Expected a to be not close to {b} but found {a}");
    }
}