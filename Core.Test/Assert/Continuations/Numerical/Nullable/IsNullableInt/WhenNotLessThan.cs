using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Continuations.Numerical.Nullable.IsNullableInt;

public class WhenNotLessThan : Spec
{
    [Theory]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    public void GivenNotLess_ThenDoesNotThrow(int? a, int b) => a.Is().NotLessThan(b);

    [Theory]
    [InlineData(1, 2)]
    public void GivenFail_ThenGetException(int? a, int b)
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().NotLessThan(b));
        ex.Message.Is("A is not less than b");
        ex.InnerException.Message.Is($"Expected a to be not less than {b} but found {a}");
    }
}