using XspecT.Assert;
using Xunit.Sdk;

namespace XspecT.Test.Assert.Numerical.IsInt;

public class WhenNot : Spec<(int a, int b), bool>
{
    [Fact] public void GivenDifferent_ThenDoesNotThrow() => 1.Is().Not(2);

    [Fact]
    public void GivenFail_ThenGetException()
    {
        int x = 1;
        var ex = Xunit.Assert.Throws<XunitException>(() => x.Is().Not(x));
        ex.InnerException.Message.Is("X is not x");
        ex.InnerException.InnerException.Message.Is("Expected x not to be 1 but found 1");
    }
}