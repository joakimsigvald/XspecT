using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNotNull : Spec
{
    [Fact]
    public void GivenNotNull_ThenDoesNotThrow()
        => new object().Is().NotNull().And.NotNull();

    [Fact]
    public void GivenNull_ThenGetException()
    {
        object actual = null;
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().NotNull());
        ex.Message.Is("Actual is not null");
        ex.InnerException.Message.Is($"Expected actual to be not null but found null");
    }
}