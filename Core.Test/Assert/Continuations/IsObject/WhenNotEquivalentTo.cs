using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotEquivalentTo : Spec
{
    [Fact]
    public void GivenNotEquivalentTo_ThenDoesNotThrow()
        => new MyRecord("abc").Is().Not().EquivalentTo(new MyOtherRecord("def")).And.Not().Null();

    [Fact]
    public void GivenEquivalentTo_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyOtherRecord("abc");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not().EquivalentTo(expected));
        ex.HasMessage($"Expected actual to not be equivalent to {expected} but found {actual}", "Actual is not equivalent to expected");
    }
}