using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.IsInt;

public class WhenGreaterThan : Spec
{
    [Fact] public void GivenGreater_ThenDoesNotThrow() => 3.Is().GreaterThan(2);

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    public void GivenFail_ThenGetException(int a, int b)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().GreaterThan(b));
        ex.Message.Is("A is greater than b");
        ex.InnerException.Message.Is($"Expected a to be greater than {b} but found {a}");
    }
}