using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotEqualTo : Spec
{
    [Fact]
    public void GivenNotEqual_ThenDoesNotThrow()
        => new MyRecord("abc").Is().Not().EqualTo(new MyRecord("def")).And.Not().Null();

    [Fact]
    public void GivenEqual_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyRecord("abc");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not().EqualTo(expected));
        ex.Message.Is("Actual is not equal to expected");
        ex.HasInnerMessage($"Expected actual to not be equal to {expected} but found {actual}");
    }
}