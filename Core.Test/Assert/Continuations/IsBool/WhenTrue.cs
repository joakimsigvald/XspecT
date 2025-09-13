using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsBool;

public class WhenTrue : Spec
{
    [Fact]
    public void GivenTrue_ThenDoesNotThrow()
        => true.Is().True().And.True();

    [Fact]
    public void GivenFalse_ThenGetException()
    {
        var actual = false;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().True());
        ex.Message.Is("Actual is true");
        ex.HasInnerMessage($"Expected actual to be true but found false");
    }
}