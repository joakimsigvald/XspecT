using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.IsInt;

public class WhenNotGreaterThan : Spec
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    public void GivenNotGreater_ThenDoesNotThrow(int a, int b) => a.Is().Not().GreaterThan(b);

    [Theory]
    [InlineData(3, 2)]
    public void GivenFail_ThenGetException(int a, int b)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().Not().GreaterThan(b));
        ex.Message.Is("A is not greater than b");
        ex.HasInnerMessage($"Expected a to not be greater than {b} but found {a}");
    }
}