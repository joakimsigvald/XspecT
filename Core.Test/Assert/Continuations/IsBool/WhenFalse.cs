using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsBool;

public class WhenFalse : Spec
{
    [Fact]
    public void GivenFalse_ThenDoesNotThrow()
        => false.Is().False().And.False();

    [Fact]
    public void GivenTrue_ThenGetException()
    {
        var actual = true;
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().False());
        ex.Message.Is("Actual is false");
        ex.InnerException.Message.Is($"Expected actual to be false but found true");
    }
}