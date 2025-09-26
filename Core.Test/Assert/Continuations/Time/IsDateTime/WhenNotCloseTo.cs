using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsDateTime;

public class WhenNotCloseTo : Spec
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(-2, 1)]
    public void GivenNotCloseTo_ThenDoesNotThrow(int days, int toleranceDays)
        => The<DateTime>().Is().Not().CloseTo(The<DateTime>().AddDays(days), TimeSpan.FromDays(toleranceDays));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = Another<DateTime>();
        var b = a.AddDays(1);
        var tolerance = TimeSpan.FromDays(1);
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().CloseTo(b, tolerance));
        ex.Message.Is("A is not close to b");
        ex.HasInnerMessage($"Expected a to not be close to {b} but found {a}");
    }
}