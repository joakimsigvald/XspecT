using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.Nullable.IsNullableInt;

public class WhenNotGreaterThan : Spec
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    public void GivenNotGreater_ThenDoesNotThrow(int? a, int b) => a.Is().NotGreaterThan(b);

    [Theory]
    [InlineData(3, 2)]
    public void GivenFail_ThenGetException(int? a, int b)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => a.Is().NotGreaterThan(b));
        ex.Message.Is("A is not greater than b");
        ex.InnerException.Message.Is($"Expected a to be not greater than {b} but found {a}");
    }
}