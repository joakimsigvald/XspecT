using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.Fractional.IsFloat;

public class WhenAround : Spec
{
    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(2, 3, 1)]
    public void GivenAround_ThenDoesNotThrow(float a, float b, float precision)
        => a.Is().Around(b, precision);

    [Theory]
    [InlineData(1, 2, 0)]
    [InlineData(1, 3, 1.99)]
    public void GivenFail_ThenGetException(float a, float b, float precision)
    {
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => a.Is().Around(b, precision));
        ex.Message.Is($"A is around b");
        ex.InnerException.Message.Is($"Expected a to be around {b} but found {a}");
    }
}