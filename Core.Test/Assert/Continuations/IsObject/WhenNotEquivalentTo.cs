using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotEquivalentTo : Spec
{
    [Fact]
    public void GivenNotEquivalentTo_ThenDoesNotThrow()
        => new MyRecord("abc").Is().NotEquivalentTo(new MyOtherRecord("def")).And.NotNull();

    [Fact]
    public void GivenEquivalentTo_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyOtherRecord("abc");
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotEquivalentTo(expected));
        ex.Message.Is("Actual is not equivalent to expected");
        ex.InnerException.Message.Is($"Expected actual to be not equivalent to {expected} but found {actual}");
    }
}