using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Numerical.IsInt;

public class WhenLessThan : Spec
{
    [Fact] public void GivenLess_ThenDoesNotThrow() => 1.Is().LessThan(2);

    [Theory]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    public void GivenFail_ThenGetException(int a, int b)
    {
        var ex = Xunit.Assert.Throws<XunitException>(() => a.Is().LessThan(b));
        ex.Message.Is("A is less than b");
        ex.InnerException.Message.Is($"Expected a to be less than {b} but found {a}");
    }
}