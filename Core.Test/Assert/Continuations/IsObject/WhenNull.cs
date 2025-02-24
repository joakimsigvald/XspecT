using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.IsObject;

public class WhenNull : Spec
{
    [Fact]
    public void GivenNull_ThenDoesNotThrow()
        => ((object)null).Is().Null();

    [Fact]
    public void GivenNotNull_ThenGetException()
    {
        var actual = new object();
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => actual.Is().Null());
        ex.Message.Is("Actual is null");
        ex.InnerException.Message.Is($"Expected actual to be null but found {actual}");
    }
}