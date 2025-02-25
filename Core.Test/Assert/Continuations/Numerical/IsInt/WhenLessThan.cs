using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.Numerical.IsInt;

public class WhenLessThan : Spec
{
    [Fact] public void GivenLess_ThenDoesNotThrow() => 1.Is().LessThan(2);

    [Theory]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    public void GivenFail_ThenGetException(int a, int b)
    {
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => a.Is().LessThan(b));
        ex.Message.Is("A is less than b");
        ex.InnerException.Message.Is($"Expected a to be less than {b} but found {a}");
    }
}