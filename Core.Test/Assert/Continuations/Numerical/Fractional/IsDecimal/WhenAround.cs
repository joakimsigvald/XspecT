using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.Fractional.IsDecimal;

public class WhenAround : Spec
{
    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(2, 3, 1)]
    public void GivenAround_ThenDoesNotThrow(decimal a, decimal b, decimal precision)
        => a.Is().Around(b, precision);

    [Theory]
    [InlineData(1, 2, 0)]
    [InlineData(1, 3, 1.99)]
    public void GivenFail_ThenGetException(decimal a, decimal b, decimal precision)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Around(b, precision));
        ex.Message.Is($"A is around b");
        ex.InnerException.Message.Is($"Expected a to be around {b} but found {a}");
    }
}