using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Numerical.IsDecimal;

public class WhenNotAround : Spec
{
    [Theory]
    [InlineData(1, 2, 0)]
    [InlineData(1, 3, 1.99)]
    public void GivenNotAround_ThenDoesNotThrow(decimal a, decimal b, decimal precision)
        => a.Is().NotAround(b, precision);

    [Theory]
    [InlineData(1, 1, 0)]
    [InlineData(2, 3, 1)]
    public void GivenFail_ThenGetException(decimal a, decimal b, decimal precision)
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotAround(b, precision));
        ex.Message.Is($"A is around b");
        ex.InnerException.Message.Is($"Expected a to be not around {b} but found {a}");
    }
}