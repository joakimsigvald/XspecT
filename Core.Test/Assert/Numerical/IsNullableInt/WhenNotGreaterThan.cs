using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Numerical.IsNullableInt;

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
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotGreaterThan(b));
        ex.Message.Is("A is not greater than b");
        ex.InnerException.Message.Is($"Expected a to be not greater than {b} but found {a}");
    }
}