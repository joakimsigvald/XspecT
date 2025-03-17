using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNot : Spec
{
    [Fact]
    public void GivenNotSame_ThenDoesNotThrow()
        => new object().Is().Not(new object()).And.Not().Null();

    [Fact]
    public void GivenNotSameButEqual_ThenDoesNotThrow()
        => new MyRecord("abc").Is().Not(new MyRecord("abc"));

    [Fact]
    public void GivenSame_ThenGetException()
    {
        var actual = new object();
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not(actual));
        ex.Message.Is("Actual is not actual");
        ex.InnerException.Message.Is($"Expected actual to be not {actual} but found {actual}");
    }

    [Fact]
    public void GivenNull_ThenGetException()
    {
        object actual = null;
        var ex = Xunit.Assert.Throws<Xunit.Sdk.XunitException>(() => actual.Is().Not(actual));
        ex.Message.Is("Actual is not actual");
        ex.InnerException.Message.Is($"Expected actual to be not null but found null");
    }
}