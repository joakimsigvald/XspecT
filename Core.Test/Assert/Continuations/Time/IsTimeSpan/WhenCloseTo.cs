using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Time.IsTimeSpan;

public class WhenCloseTo : Spec
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    public void GivenCloseTo_ThenDoesNotThrow(int days, int toleranceDays)
        => A<TimeSpan>().Is().CloseTo(The<TimeSpan>().Add(TimeSpan.FromDays(days)), TimeSpan.FromDays(toleranceDays));

    [Fact]
    public void GivenFail_ThenGetException()
    {
        var a = A<TimeSpan>();
        var b = a.Add(TimeSpan.FromDays(1));
        var tolerance = TimeSpan.FromDays(0);
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => a.Is().CloseTo(b, tolerance));
        ex.Message.Is("A is close to b");
        ex.InnerException.Message.Is($"Expected a to be close to {b} but found {a}");
    }
}