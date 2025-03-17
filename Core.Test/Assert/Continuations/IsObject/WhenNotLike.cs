using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotLike : Spec
{
    [Fact]
    public void GivenNotLike_ThenDoesNotThrow()
        => new MyRecord("abc").Is().Not().Like(new MyOtherRecord("def")).And.Not().Null();

    [Fact]
    public void GivenLike_ThenGetException()
    {
        var actual = new MyRecord("abc");
        var expected = new MyOtherRecord("abc");
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not().Like(expected));
        ex.Message.Is("Actual is not like expected");
        ex.InnerException.Message.Is($"Expected actual to not be like {expected} but found {actual}");
    }
}