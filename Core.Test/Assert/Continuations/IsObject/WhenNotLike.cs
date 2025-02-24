using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotLike : Spec
{
    [Fact]
    public void GivenNotLike_ThenDoesNotThrow()
        => new MyRecord("abc").Is().NotLike(new MyOtherRecord("def")).And.NotNull();

    [Fact]
    public void GivenLike_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyOtherRecord("abc");
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotLike(expected));
        ex.Message.Is("Actual is not like expected");
        ex.InnerException.Message.Is($"Expected actual to be not like {expected} but found {actual}");
    }
}