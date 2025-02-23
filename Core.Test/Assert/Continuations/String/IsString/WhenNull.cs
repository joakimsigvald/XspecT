using XspecT.Assert;

namespace XspecT.Test.Assert.Continuations.String.IsString;

public class WhenNull : StringSpec
{
    [Fact]
    public void GivenNull_ThenDoesNotThrow()
        => ((string)null).Is().Null().And.Null();

    [Fact]
    public void GivenNotNull_ThenGetException()
    {
        var str = "";
        var ex = Xunit.Assert.Throws<AssertionFailed>(() => str.Is().Null());
        ex.Message.Is("Str is null");
        ex.InnerException.Message.Is($"Expected str to be null but found {Describe(str)}");
    }
}