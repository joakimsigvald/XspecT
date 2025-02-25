using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenEqualTo : Spec
{
    [Fact]
    public void GivenEqualTo_ThenDoesNotThrow()
        => new MyRecord("abc").Is().EqualTo(new MyRecord("abc")).And.NotNull();

    [Fact]
    public void GivenNotEqualTo_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyRecord("def");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().EqualTo(expected));
        ex.Message.Is("Actual is equal to expected");
        ex.InnerException.Message.Is($"Expected actual to be equal to {expected} but found {actual}");
    }
}