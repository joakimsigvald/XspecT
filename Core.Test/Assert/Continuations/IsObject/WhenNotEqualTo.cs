using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotEqualTo : Spec
{
    [Fact]
    public void GivenNotEqual_ThenDoesNotThrow()
        => new MyRecord("abc").Is().NotEqualTo(new MyRecord("def")).And.NotNull();

    [Fact]
    public void GivenEqual_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyRecord("abc");
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotEqualTo(expected));
        ex.Message.Is("Actual is not equal to expected");
        ex.InnerException.Message.Is($"Expected actual to be not equal to {expected} but found {actual}");
    }
}